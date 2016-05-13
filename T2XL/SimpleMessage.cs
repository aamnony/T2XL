using System;

namespace T2XL
{
    internal struct SimpleMessage
    {
        public SimpleMessage(int id, DateTime dateTime, SimpleUser user, string message)
        {
            Id = id;
            Time = dateTime;
            User = user;
            Message = message;
            Chat = new SimpleUser();
        }

        public SimpleUser Chat { get; set; }
        public int Id { get; private set; }
        public string Message { get; private set; }
        public DateTime Time { get; private set; }
        public SimpleUser User { get; set; }
    }
}