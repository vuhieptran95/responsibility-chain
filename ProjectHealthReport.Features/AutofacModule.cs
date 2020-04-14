using System.Reflection;
using Autofac;
using DinkToPdf;
using DinkToPdf.Contracts;
using ResponsibilityChain;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Authorizations;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.EventsHandlers;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.Logging;
using ResponsibilityChain.Business.RequestContexts;
using ResponsibilityChain.Business.Validations;
using Module = Autofac.Module;

namespace ProjectHealthReport.Features
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterType<Mediator>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomScope>()
                .AsSelf()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<RequestContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(RequestHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(AuthorizationConfig<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(AuthorizationHandlerBase<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(AuthorizationHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(AuthorizationExceptionHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(DefaultBranchHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ValidationHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ExecutionHandlerBase<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(LoggingHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EventsHandler<,>)).InstancePerLifetimeScope();
            
            builder.RegisterGeneric(typeof(CacheHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(CacheConfig<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(PreEvent<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(PostEvent<,>)).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(Handler<,>))
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(IRequest<>));
            base.Load(builder);
            
            builder.RegisterType<SynchronizedConverter>().As<IConverter>().SingleInstance();
            builder.RegisterType<PdfTools>().As<ITools>().SingleInstance();
        }
    }
}