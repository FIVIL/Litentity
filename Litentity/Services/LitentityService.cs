using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Litentity.Services
{
    public static class LitentityService
    {
        private static readonly LitentityConfiguration Conf = new LitentityConfiguration();
        internal static void SetPrivateResponseCredentails(Guid Key)
        {

        }
        public static void AddLitentity<T>(this IServiceCollection service,Action<LitentityConfiguration> configuration)
            where T:Litentity.Sessions.SessionData
        {
            service.AddSingleton<Sessions.SessionProvider>();
            service.AddScoped<ILitentity<T>, Services.Litentity<T>>();
            configuration(Conf);
        }
        public static void AddLitentity<T>(this IServiceCollection service)
     where T : Litentity.Sessions.SessionData
        {
            service.AddSingleton<Sessions.SessionProvider>();
            service.AddScoped<ILitentity<T>, Services.Litentity<T>>();
        }
    }
}
