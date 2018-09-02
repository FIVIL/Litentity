using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace FIVIL.Litentity
{
    public class LitentityLoginAttribute : Attribute, IAuthorizationFilter
    {
        private readonly LitentityUsersTypes _type;
        public LitentityLoginAttribute(LitentityUsersTypes types = LitentityUsersTypes.NormalUser)
        {
            _type = types;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Litentity<SessionData> SB = (Litentity<SessionData>)context.HttpContext.RequestServices.GetRequiredService<ILitentity<SessionData>>();
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }
            if (!SB.status) context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "Unauthorized"
            };
            if (SB.SB.User.UserType != _type)
            {
                if (!SB.status) context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = $"Unauthorized only {_type.ToString()} Allowed."
                };
            }
        }
    }
}
