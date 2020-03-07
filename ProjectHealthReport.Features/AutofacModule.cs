using System.Reflection;
using Autofac;
using ResponsibilityChain;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.Authorizations;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.Logging;
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

            builder.RegisterGeneric(typeof(RequestHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(AuthorizationHandlerBase<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(AuthorizationHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(AuthorizationExceptionHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(AuthorizationDefaultHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ValidationHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ExecutionHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(LoggingHandler<,>)).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(Handler<,>))
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(IRequest<>));
            base.Load(builder);
        }
    }
}