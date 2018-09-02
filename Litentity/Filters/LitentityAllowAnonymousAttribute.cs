﻿
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FIVIL.Litentity
{
    class LitentityAllowAnonymousAttribute : Attribute, IAuthorizationFilter, IAllowAnonymousFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Litentity<SessionData> SB = (Litentity<SessionData>)context.HttpContext.RequestServices.GetRequiredService<ILitentity<SessionData>>();
            SB.status = true;
        }
    }
}
