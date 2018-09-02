
using System;
using System.Collections.Generic;
using System.Text;

namespace FIVIL.Litentity
{
    class SessionBase
    {
        internal SessionBase(LitentityUsers user)
        {
            User = user;
            LastSeen = DateTime.Now;
        }

        internal readonly LitentityUsers User;

        public string UserName => User.UserName;
        public string Email => User.EmailAddress;
        public string PhoneNumber => User.PhoneNumber;
        public bool EmailConfirmed => User.EmailConfirmed;
        public bool PhoneConfirmed => User.PhoneConfirmed;

        public SessionData SessionData;

        internal DateTime LastSeen;
    }
}
