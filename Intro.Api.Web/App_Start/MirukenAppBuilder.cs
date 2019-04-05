using System;
using System.Web;
using Miruken.AspNet;
using Miruken.Context;
using Owin;

namespace Intro.Api.Web
{
    public static class MirukenAppBuilder
    {
        public static IAppBuilder UseMiruken(
            this IAppBuilder builder, Context context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            return builder.Use(async (owin, next) =>
            {
                var env = owin.Environment;
                if (env.TryGetValue("System.Web.HttpContextBase", out var http))
                    (http as HttpContextBase)?.UseMiruken(context);
                await next();
            });
        }
    }
}