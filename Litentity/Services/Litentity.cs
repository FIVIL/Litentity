using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FIVIL.Litentity
{
    public class Litentity<T> : ILitentity<T> where T : SessionData
    {
        internal Guid Key { get; set; }
        internal SessionProvider SessionS;
        internal SessionBase SB;
        internal bool status;
        public T Session => (T)SB.SessionData;

        public string UserName => SB.UserName;

        public string Email => SB.Email;

        public string PhoneNumber => SB.PhoneNumber;

        public bool PhoneConfirmed => SB.PhoneConfirmed;

        public bool EmailConfirmed => SB.EmailConfirmed;

        public bool IsLogedIn(Guid key) => SessionS.Get(key).Item1;

        public bool Login(LitentityUsers SB)
        {
            var sb = new SessionBase(SB);
            var p = SessionS.Add(sb);
            LitentityService.SetPrivateResponseCredentails(p.Item2);
            return p.Item1;
        }

        public bool LogOff() => SessionS.Remove(Key);

        public static Guid UpdatePublicToken()
        {
            LitentityConfiguration.PublicToken = Guid.NewGuid();
            return LitentityConfiguration.PublicToken;
        }
        public static void UpdatePublicTokenName(string publicTokenName)
        {
            LitentityConfiguration.PublicTokenName = publicTokenName;
        }
        public static void UpdatePrivateTokenName(string privateTokenName)
        {
            LitentityConfiguration.PrivateTokenName = privateTokenName;
        }
    }
}
