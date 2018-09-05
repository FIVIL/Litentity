
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FIVIL.Litentity
{
    public class LitentityAllowAnonymousAttribute : Attribute, IAuthorizationFilter, IAllowAnonymousFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Litentity SB = (Litentity)context.HttpContext.RequestServices.GetRequiredService<ILitentity>();
            SB.status = true;
        }
    }
}
