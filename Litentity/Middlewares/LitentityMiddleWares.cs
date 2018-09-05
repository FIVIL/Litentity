using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace FIVIL.Litentity
{
    public static class LitentityMiddleWares
    {
        public static void UseLitentityPrivateAuthentication(this IApplicationBuilder app)
        {
            app.Use(async (c, n) =>
            {
                bool flag = false;
                Guid g = Guid.Empty;
                var sessions = c.RequestServices.GetRequiredService<SessionProvider>();
                sessions.Initiate();
                var SB = (Litentity)c.RequestServices.GetRequiredService<ILitentity>();
                SB.SessionS = sessions;
                if (LitentityConfiguration.Cookie)
                {
                    if (c.Request.Cookies.ContainsKey(LitentityConfiguration.PrivateTokenName))
                    {
                        var p = c.Request.Cookies[LitentityConfiguration.PrivateTokenName].ToString();
                        if (Guid.TryParse(p, out g))
                        {
                            var sb = sessions.Get(g);
                            if (sb.Item1)
                            {
                                SB.Key = g;
                                SB.SB = sb.Item2;
                                flag = true;
                                SB.status = flag;
                                SB.SB.LastSeen = DateTime.Now;
                            }
                            else
                            {
                                flag = false;
                                SB.status = flag;
                            }
                        }
                    }
                }
                if (!flag)
                {
                    if (LitentityConfiguration.Header)
                    {
                        if (c.Request.Headers.ContainsKey(LitentityConfiguration.PrivateTokenName))
                        {
                            var p = c.Request.Headers[LitentityConfiguration.PrivateTokenName].ToString();
                            if (Guid.TryParse(p, out g))
                            {
                                var sb = sessions.Get(g);
                                if (sb.Item1)
                                {
                                    SB.Key = g;
                                    SB.SB = sb.Item2;
                                    flag = true;
                                    SB.status = flag;
                                    SB.SB.LastSeen = DateTime.Now;
                                }
                                else
                                {
                                    flag = false;
                                    SB.status = flag;
                                }

                            }
                        }
                    }
                }
                await n.Invoke();
                if (LitentityConfiguration.Header)
                {
                    c.Response.Headers.Add(LitentityConfiguration.PrivateTokenName, g.ToString());
                }
                if (LitentityConfiguration.Cookie)
                {
                    c.Response.Cookies.Append(LitentityConfiguration.PrivateTokenName, g.ToString());
                }
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TokenGeneratorPath">Url for requesting new Tokens</param>
        /// <param name="Condition">certain condition that have to meet before user can request for ApiToken.should return true if condidtion have met. or else if not.</param>
        public static void UseLitentityPublicTokens(this IApplicationBuilder app, string TokenGeneratorPath, Func<bool> Condition = null)
        {
            app.Map($"/{TokenGeneratorPath}", app2 =>
            {
                var cond = Condition?.Invoke() ?? true;
                if (cond)
                {
                    app2.Run(async c =>
                    {
                        c.Response.ContentType = "application/json";
                        c.Response.StatusCode = 200;
                        await c.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            TokenName = LitentityConfiguration.PublicTokenName,
                            ApiToken = LitentityConfiguration.PublicToken,
                            HowToUse = "You should put ApiToken in request headers whith TokenName as key."
                        }));
                    });
                }
                else
                {
                    app2.Run(async c =>
                    {
                        c.Response.ContentType = "application/json";
                        c.Response.StatusCode = 400;
                    });
                }
            });
            app.Use(async (c, n) =>
            {
                if (c.Request.Headers.ContainsKey(LitentityConfiguration.PublicTokenName))
                {
                    var p = c.Request.Headers[LitentityConfiguration.PublicTokenName];
                    if (Guid.TryParse(p, out var g))
                    {
                        if (g == LitentityConfiguration.PublicToken)
                        {
                            await n.Invoke();
                            c.Response.Headers.Add(LitentityConfiguration.PublicTokenName, LitentityConfiguration.PublicToken.ToString());
                            return;
                        }
                    }
                }
                c.Response.ContentType = "application/json";
                c.Response.StatusCode = 401;
                await c.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    Reason = "You Should Provide Api public Token",
                    How = $"send a get request to {c.Request.PathBase}/{TokenGeneratorPath} whit certan condition."
                }));
            });
        }
    }
}
