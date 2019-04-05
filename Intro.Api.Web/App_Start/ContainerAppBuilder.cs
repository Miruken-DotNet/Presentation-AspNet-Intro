using System;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Services.Logging.NLogIntegration;
using Castle.Windsor;
using Miruken.Castle;
using Owin;

namespace Intro.Api.Web
{
    public static class ContainerAppBuilder
    {
        private const string WindsorKey = "Castle.Windsor";

        public static IAppBuilder UseWindsor(
            this IAppBuilder builder, Action<IWindsorContainer> config)
        {
            var container = builder.GetContainer();
            config?.Invoke(container);
            return builder;
        }

        public static IWindsorContainer GetContainer(this IAppBuilder builder)
        {
            if (builder.Properties.TryGetValue(WindsorKey, out var container) == false)
            {
                var windsor = new WindsorContainer();
                windsor.Kernel.Resolver.AddSubResolver(new CollectionResolver(windsor.Kernel, true));
                windsor.Kernel.AddHandlersFilter(new ContravariantFilter());
                windsor.AddFacility<LoggingFacility>(f => f.LogUsing<NLogFactory>());
                builder.Properties[WindsorKey] = windsor;
                container = windsor;
            }
            return (IWindsorContainer)container;
        }
    }
}