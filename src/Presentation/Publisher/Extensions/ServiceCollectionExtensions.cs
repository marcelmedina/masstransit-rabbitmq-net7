using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Publisher.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMassTransitPublisher(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddDelayedMessageScheduler();
                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration.GetValue<string>("RabbitMq"));

                    cfg.UseDelayedMessageScheduler();
                    cfg.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));

                    cfg.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));
                });
            });

            return services;
        }
    }
}
