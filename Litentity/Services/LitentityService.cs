using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FIVIL.Litentity
{
    public static class LitentityService
    {
        private static readonly LitentityConfiguration Conf = new LitentityConfiguration();
        internal static void SetPrivateResponseCredentails(Guid Key)
        {

        }
        public static void AddLitentity<T>(this IServiceCollection service,Action<LitentityConfiguration> configuration)
            where T:SessionData
        {
            service.AddSingleton<SessionProvider>();
            service.AddScoped<ILitentity<T>, Litentity<T>>();
            configuration(Conf);
        }
        public static void AddLitentity<T>(this IServiceCollection service)
     where T : SessionData
        {
            service.AddSingleton<SessionProvider>();
            service.AddScoped<ILitentity<T>, Litentity<T>>();
        }
    }
}
