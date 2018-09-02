
using System;
using System.Collections.Generic;
using System.Text;

namespace FIVIL.Litentity
{
    public interface ILitentity<T> where T : SessionData
    {
        T Session { get; }
        string UserName { get; }
        string Email { get; }
        string PhoneNumber { get; }
        bool PhoneConfirmed { get; }
        bool EmailConfirmed { get; }
        bool IsLogedIn(Guid key);
        bool Login(LitentityUsers SB);
        bool LogOff();
    }
}
