using Subscriber.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMassTransitPublisher(context.Configuration);
    })
    .Build();

host.Run();
