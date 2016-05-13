using System;
using TLSharp.Core.MTProto;

namespace T2XL
{
    internal static class Utils
    {
        private static readonly DateTime UNIX_BASE_TIME = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        internal static DateTime FromUnixTime(int unix)
        {
            return UNIX_BASE_TIME.AddSeconds(unix);
        }

        internal static int ToUnixTime(DateTime dt)
        {
            return (int)Math.Truncate((dt.ToUniversalTime() - UNIX_BASE_TIME).TotalSeconds);
        }

        internal static string Combine(string message, MessageMedia media)
        {
            string s = message;
            if (media != null && media.Constructor != Constructor.messageMediaEmpty)
            {
                if (!String.IsNullOrEmpty(s))
                {
                    s += " ";
                }
                s += '<' + media.Constructor.ToString() + '>';
            }
            return s;
        }

        internal static string Forwarded(string message, int fwd_date, SimpleUser fwd_from_user)
        {
            return String.Format("<Quoted message by {0} on {1}>\n{2}", fwd_from_user.Name, FromUnixTime(fwd_date).ToShortDateString(), message);
        }
    }
}