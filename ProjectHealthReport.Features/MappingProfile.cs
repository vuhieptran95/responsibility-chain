using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Features
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetAssembly(typeof(Project)));
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            ApplyMappingsToAssembly(Assembly.GetAssembly(typeof(Project)));
            ApplyMappingsToAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methods = type.GetMethods();
                var interfaces = type.GetInterfaces();

                var methodInfo = type.GetMethod("MappingFrom")
                                 ?? type.GetInterface("IMapFrom`1").GetMethod("MappingFrom");

                methodInfo?.Invoke(instance, new object[] {this});
            }
        }
        
        private void ApplyMappingsToAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methods = type.GetMethods();
                var interfaces = type.GetInterfaces();

                var methodInfo = type.GetMethod("MappingTo")
                                 ?? type.GetInterface("IMapTo`1").GetMethod("MappingTo");

                methodInfo?.Invoke(instance, new object[] {this});
            }
        }
    }
}