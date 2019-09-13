using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NBB.Application.DataContracts;
using NBB.Core.Abstractions;
using NBB.Core.DependencyInjection;
using NBB.Domain.Abstractions;
using Pet.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Pet.ExpenseTracking.Domain;

namespace Pet.Application
{
    public static class DependencyInjectionExtensions
    {
        public static void AddPetApplication(this IServiceCollection services)
        {
            services.AddDomainServices();
            services.AddScoped<IEventHub, EventHub>();
            services.AddMediatR(typeof(DependencyInjectionExtensions).Assembly);
            services.AddScopedContravariant<INotificationHandler<INotification>, EventContextLogger>(typeof(ExpenseTracking.Domain.DependencyInjectionExtensions).Assembly);

            services.DecorateOpenGenericWhen(typeof(IUow<>), typeof(DomainUowDecorator<>),
                serviceType => typeof(IEventedAggregateRoot).IsAssignableFrom(serviceType.GetGenericArguments()[0]));
            services.DecorateOpenGenericWhen(typeof(IUow<>), typeof(MediatorUowDecorator<>),
                serviceType => typeof(IEventedEntity).IsAssignableFrom(serviceType.GetGenericArguments()[0]));
        }

        private static void AddScopedContravariant<TBase, TResolve>(this IServiceCollection serviceCollection, Assembly assembly = null)
        {
            if (!typeof(TBase).IsGenericType || typeof(TBase).IsOpenGeneric())
                return;

            var baseDescription = typeof(TBase).GetGenericTypeDefinition();
            var baseInnerType = typeof(TBase).GetGenericArguments().First();

            var types = (assembly ?? baseInnerType.Assembly).ScanFor(baseInnerType);
            foreach (var t in types)
                serviceCollection.AddScoped(baseDescription.MakeGenericType(t), typeof(TResolve));
        }

        private static IEnumerable<Type> ScanFor(this Assembly assembly, Type assignableType)
        {
            return assembly.GetTypes().Where(t => assignableType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
        }
    }
}
