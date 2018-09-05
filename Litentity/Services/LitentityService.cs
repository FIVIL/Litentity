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
        public static void AddLitentity(this IServiceCollection service,Action<LitentityConfiguration> configuration)
        {
            service.AddSingleton<SessionProvider>();
            service.AddScoped<ILitentity, Litentity>();
            configuration(Conf);
        }
        public static void AddLitentity(this IServiceCollection service)
        {
            service.AddSingleton<SessionProvider>();
            service.AddScoped<ILitentity, Litentity>();
        }
    }
}
