using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Castle.Core.Logging;
using Miruken.AspNet;
using Miruken.AspNet.Castle;
using Miruken.Context;

namespace Intro.Api.Web
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register(
            Context context, ILogger logger)
        {
            var config = new HttpConfiguration()
                .UseMiruken(context);

            SwaggerConfig.Register(config);

            config.Services.Add(typeof(IExceptionLogger),
                new UnhandledExceptionLogger(logger));

            config.MapHttpAttributeRoutes();

            return config;
        }
    }
}
