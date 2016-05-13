using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using TLSharp.Core;
using TLSharp.Core.MTProto;

namespace T2XL
{
    public partial class MainForm : Form
    {
        private readonly string ExcelFilePath = ConfigurationManager.AppSettings["ExcelFilePath"];
        private readonly int MsgCount = int.Parse(ConfigurationManager.AppSettings["MsgCount"]);
        private readonly int Iterations = int.Parse(ConfigurationManager.AppSettings["Naglot"]);
        private readonly bool ShowExcelUI = bool.Parse(ConfigurationManager.AppSettings["ShowExcelUI"]);

        private TelegramClient mClient;
        private string mHash;
        private int mSelfUserId;
        private int mStartId = 1;
        private FileSessionStore mSessionStore;
        private User mUser;
        private List<SimpleUser> mUsers;
        private XL xl;

        public MainForm()
        {
            InitializeComponent();
        }

        private async Task<SimpleUser> AddGroupToUsers(int chatId)
        {
            var chat = await mClient.GetChats(new List<int> { chatId });
            var group = new SimpleUser(chatId, ((ChatConstructor)chat[0]).title);
            mUsers.Add(group);
            mUsers.Sort();
            return group;
        }

        private async Task AddMessagesToExcel()
        {
            for (int i = 1; i <= Iterations; i++)
            {
                lblWorking.Text = String.Format("Adding messages...x{0}{1}", i, i>5 ? "\n holy shiit :P" : "");
                var history = await mClient.GetMessages(Enumerable.Range(mStartId, MsgCount).ToList());
                if (history.Count <= 0)
                {
                    break;
                }

                foreach (var msg in history)
                {
                    Type t = msg.GetType();
                    if (t == typeof(MessageServiceConstructor))
                    {
                        continue;
                    }
                    else if (t == typeof(MessageForwardedConstructor))
                    {
                        MessageForwardedConstructor m = (MessageForwardedConstructor)msg;
                        var def = new SimpleUser(m.fwd_from_id);
                        int fwd_from_user = mUsers.BinarySearch(def);
                        
                        var sm = await ToSimpleMessage(m.id, m.to_id, m.from_id, m.date, m.output, 
                            Utils.Combine(Utils.Forwarded(m.message, m.fwd_date, fwd_from_user >= 0 ? mUsers[fwd_from_user] : def), m.media));

                        xl.AddMessage(sm);
                    }
                    else if (t == typeof(MessageEmptyConstructor))
                    {
                        continue;
                    }
                    else if (t == typeof(MessageConstructor))
                    {
                        MessageConstructor m = (MessageConstructor)msg;
                        var sm = await ToSimpleMessage(m.id, m.to_id, m.from_id, m.date, m.output, Utils.Combine(m.message, m.media));
                        xl.AddMessage(sm);
                    }
                }
                UpdateStartIdFromExcel();
            }
            xl.Close();
            Close();
        }

        private void UpdateStartIdFromExcel()
        {
            mStartId = (1 + xl.GetLastId());
        }

        private async void btnSendCode_Click(object sender, EventArgs e)
        {
            mHash = await mClient.SendCodeRequest(mtxtPhoneNumber.Text);
        }

        private async Task<List<SimpleUser>> GetUsers()
        {
            var ids = (await mClient.GetContacts()).Cast<ContactConstructor>();
            var ius = ids.Select(r => TL.inputUserContact(r.user_id)).ToList();

            var users = new List<SimpleUser>(ius.Count);
            foreach (var user in ius)
            {
                var su = new SimpleUser(await mClient.GetFullUser(user));
                if (su.Self)
                {
                    mSelfUserId = su.Id;
                }
                users.Add(su);
            }
            users.Sort();
            return users;
        }

        private async Task Login()
        {
            grpLogin.Visible = false;
            mUsers = await GetUsers();
            //TerminateExcel();
            if (System.IO.File.Exists(ExcelFilePath))
            {
                xl = new XL(ExcelFilePath, ShowExcelUI);
                UpdateStartIdFromExcel();
            }
            else
            {
                xl = new XL(mSelfUserId, ExcelFilePath, ShowExcelUI);
            }
            await AddMessagesToExcel();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            //mtxtPhoneNumber.Text = "972524880330";
            Text = Assembly.GetExecutingAssembly().GetName().Name;

            mSessionStore = new FileSessionStore();
            mClient = new TelegramClient(mSessionStore, "session");
            await mClient.Connect();

            if (mClient.IsUserAuthorized())
            {
                await Login();
            }
        }

        private async void mtxtCode_TextChanged(object sender, EventArgs e)
        {
            if (mtxtCode.Text.Length == mtxtCode.Mask.Length)
            {
                mUser = await mClient.MakeAuth(mtxtPhoneNumber.Text, mHash, mtxtCode.Text);

                if (mClient.IsUserAuthorized())
                {
                    await Login();
                }
                else
                {
                    lblWrongCode.Visible = true;
                }
            }
        }

        private async Task<SimpleMessage> ToSimpleMessage(int id, Peer to_id, int from_id, int date, bool output, string message)
        {
            int i = mUsers.BinarySearch(new SimpleUser(from_id));
            var sm = new SimpleMessage(id,
                Utils.FromUnixTime(date),
                i >= 0 ? mUsers[i] : new SimpleUser(from_id),
                message);

            if (to_id.Constructor == Constructor.peerChat)
            {
                // Set the simple message chat (conversation) to the name of the group.
                var chatId = ((PeerChatConstructor)to_id).chat_id;
                var group = new SimpleUser(chatId);
                int j = mUsers.BinarySearch(group);
                if (j < 0)
                {
                    sm.Chat = await AddGroupToUsers(chatId);
                }
                else
                {
                    sm.Chat = mUsers[j];
                }
            }
            else
            {
                // Set the simple message chat (conversation) to the name of the opposite user (not the logged in user).
                var def = new SimpleUser(((PeerUserConstructor)to_id).user_id);
                int k = mUsers.BinarySearch(def);
                sm.Chat = output ? (k >= 0 ? mUsers[k] : def) : sm.User;
            }

            return sm;
        }
    }
}