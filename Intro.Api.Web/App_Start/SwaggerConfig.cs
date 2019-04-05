namespace Intro.Api.Web
{
    using Miruken.AspNet.Swagger;
    using Swashbuckle.Application;
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Web.Http;

    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            config.EnableSwagger(swagger =>
            {
                swagger.RootUrl(GetRootUrl);
                swagger.SingleApiVersion("v1", "Intro.Api");
                swagger.UseMiruken();
            })
            .EnableSwaggerUi();
        }

        private static string GetRootUrl(HttpRequestMessage request)
        {
            return new UriBuilder(
                    request.RequestUri.Scheme,
                    request.RequestUri.Host,
                    int.Parse(request.RequestUri.Port.ToString(
                        CultureInfo.InvariantCulture)),
                    request.GetRequestContext().VirtualPathRoot).
                Uri.AbsoluteUri.TrimEnd('/');
        }
    }
}
