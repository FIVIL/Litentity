using System;
using System.Collections.Generic;
using System.Text;

namespace FIVIL.Litentity
{
    public class LitentityConfiguration
    {
        internal static bool Cookie = false;
        internal static bool Header = true;
        internal static string PrivateTokenName = "LPT";
        internal static string PublicTokenName = "LPuT";
        internal static Action<LitentityUsers> callBack;
        internal static TimeSpan IdleTime = TimeSpan.FromMinutes(40);
        internal static Guid PublicToken = Guid.NewGuid();
        internal static Type type;

        public LitentityConfiguration()
        {

        }
        public LitentityConfiguration UseCookie(bool Use)
        {
            Cookie = Use;
            return this;
        }
        public LitentityConfiguration UserHeader(bool Use)
        {
            Header = Use;
            return this;
        }
        public LitentityConfiguration SetPublicTokenName(string publicTokenName)
        {
            PublicTokenName = publicTokenName;
            return this;
        }
        public LitentityConfiguration SetPrivateTokenName(string privateTokenName)
        {
            PrivateTokenName = privateTokenName;
            return this;
        }
        public LitentityConfiguration SetCallBackFunction(Action<LitentityUsers> CallBack)
        {
            callBack = CallBack;
            return this;
        }
        public LitentityConfiguration SetIdleTimeOut(TimeSpan idle)
        {
            IdleTime = idle;
            return this;
        }
    }
}
