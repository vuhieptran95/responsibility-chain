﻿using System.Reflection;
using Autofac;
using ResponsibilityChain;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.Auditing;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Authorizations;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Events;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.Logging;
using ResponsibilityChain.Business.Processors;
using ResponsibilityChain.Business.RequestContexts;
using ResponsibilityChain.Business.Validations;
using Module = Autofac.Module;

namespace IdentityServer.Features
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
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(DefaultHandler<,>)).InstancePerLifetimeScope();
            
            builder.RegisterGeneric(typeof(RequestHandler<,>)).InstancePerLifetimeScope();
            
            
            builder.RegisterGeneric(typeof(AuthorizationConfigBase<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(AuthorizationHandler<,>)).InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(IPreAuthorizationRule<,>))
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(IPostAuthorizationRule<,>))
                .InstancePerLifetimeScope();
            
            builder.RegisterGeneric(typeof(DefaultHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ValidationHandlerBase<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ExecutionHandlerBase<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(LoggingHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(AuditingHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EventsHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ProcessorHandlerBase<,>)).InstancePerLifetimeScope();
            
            builder.RegisterGeneric(typeof(CacheHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(CacheConfig<>))
                .As(typeof(ICacheConfig<>))
                .InstancePerLifetimeScope();
            
            builder.RegisterGeneric(typeof(DefaultAuthorizationConfig<>))
                .As(typeof(IAuthorizationConfig<>))
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(ICacheConfig<>))
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(IAuthorizationConfig<>))
                .InstancePerLifetimeScope();
                
            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(IPostProcessor<,>))
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(IPreProcessor<,>))
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(IPreEvent<,>))
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(IPostEvent<,>))
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(IPreAudit<,>))
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(IPostAudit<,>))
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(IExecution<,>))
                .InstancePerLifetimeScope();

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