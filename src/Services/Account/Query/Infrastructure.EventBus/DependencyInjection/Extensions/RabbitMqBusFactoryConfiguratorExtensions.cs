﻿using Contracts.Abstractions.Messages;
using Contracts.Services.Account;
using Infrastructure.EventBus.Consumers;
using MassTransit;

namespace Infrastructure.EventBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        cfg.ConfigureEventReceiveEndpoint<AccountCreatedConsumer, DomainEvent.AccountCreated>(context);
        cfg.ConfigureEventReceiveEndpoint<AccountDeletedConsumer, DomainEvent.AccountDeleted>(context);
        cfg.ConfigureEventReceiveEndpoint<BillingAddressAddedConsumer, DomainEvent.BillingAddressAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ShippingAddressAddedConsumer, DomainEvent.ShippingAddressAdded>(context);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext context)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"account.query.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(context);
            });
}