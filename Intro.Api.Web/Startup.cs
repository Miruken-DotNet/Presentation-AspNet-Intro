using System.Net;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Intro.Api.Student;
using Miruken.AspNet.Castle;
using Miruken.Castle;
using Miruken.Context;
using Miruken.Validate;
using Miruken.Validate.Castle;
using Owin;

namespace Intro.Api.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var appContext = new Context();
            var applicationName = Assembly.GetExecutingAssembly().GetName().Name;
            NLog.GlobalDiagnosticsContext.Set("ApplicationName", applicationName);

            app.UseWindsor(container =>
            {
                var logger = container.Resolve<ILogger>();
                logger.InfoFormat("Started {0}", applicationName);

                container.Install(new FeaturesInstaller(
                        new ConfigurationFeature(),
                        new ValidateFeature(),
                        new AspNetFeature(),
                        new HandleFeature().AddFilters(
                            new Miruken.Callback.FilterAttribute(typeof(LogFilter<,>)),
                            new Miruken.Callback.FilterAttribute(typeof(ValidateFilter<,>))
                        )).Use(
                        Classes.FromThisAssembly(),
                        Classes.FromAssemblyContaining<StudentHandler>(),
                        Classes.FromAssemblyContaining<Authorization>())
                );
                appContext.AddHandlers(new WindsorHandler(container));

                app.UseWebApi(WebApiConfig.Register(appContext, logger))
                   .UseMiruken(appContext);
            });

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
