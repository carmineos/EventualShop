﻿using ECommerce.JsonConverters;
using JsonNet.ContractResolvers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.EventSourcing.EventStore.Contexts.Converters;

public class CatalogConverter : ValueConverter<Domain.Aggregates.Catalog, string> 
{
    public CatalogConverter()
        : base(
            @event => JsonConvert.SerializeObject(@event, typeof(Domain.Aggregates.Catalog), SerializerSettings()),
            jsonString => JsonConvert.DeserializeObject<Domain.Aggregates.Catalog>(jsonString, DeserializerSettings())) { }

    private static JsonSerializerSettings SerializerSettings()
    {
        var jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

        jsonSerializerSettings.Converters.Add(new DateOnlyJsonConverter());
        jsonSerializerSettings.Converters.Add(new ExpirationDateOnlyJsonConverter());

        return jsonSerializerSettings;
    }

    private static JsonSerializerSettings DeserializerSettings()
    {
        var jsonDeserializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ContractResolver = new PrivateSetterContractResolver()
        };

        jsonDeserializerSettings.Converters.Add(new DateOnlyJsonConverter());
        jsonDeserializerSettings.Converters.Add(new ExpirationDateOnlyJsonConverter());

        return jsonDeserializerSettings;
    }
}