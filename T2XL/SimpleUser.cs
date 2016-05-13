using System;
using TLSharp.Core.MTProto;

namespace T2XL
{
    internal class SimpleUser : IComparable<SimpleUser>
    {
        public SimpleUser(int id, string name, string phone)
        {
            Id = id;
            Name = name;
            Phone = phone;
        }

        public SimpleUser() : this(0, String.Empty, String.Empty)
        {
        }

        public SimpleUser(int id) : this(id, String.Empty, String.Empty)
        {
        }

        public SimpleUser(int id, string chatTitle) : this(id, chatTitle, String.Empty)
        {
        }

        public SimpleUser(User user)
        {
            Type t = user.GetType();
            if (t == typeof(UserContactConstructor))
            {
                var u = ((UserContactConstructor)user);
                Id = u.id;
                Name = u.first_name + (String.IsNullOrWhiteSpace(u.last_name) ? String.Empty : ' ' + u.last_name);
                Phone = u.phone;
            }
            else if (t == typeof(UserSelfConstructor))
            {
                var u = ((UserSelfConstructor)user);
                Id = u.id;
                Name = u.first_name + (String.IsNullOrWhiteSpace(u.last_name) ? String.Empty : ' ' + u.last_name);
                Phone = u.phone;
                Self = true;
            }
            else
            {
                throw new ArgumentException(String.Format("User type '{0}' is not supported", user.Constructor));
            }
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public bool Self { get; private set; }

        public int CompareTo(SimpleUser other)
        {
            return Id.CompareTo(other.Id);
        }

        public override string ToString()
        {
            return String.Format("{0} - {1}", Name, Id);
        }
    }
}