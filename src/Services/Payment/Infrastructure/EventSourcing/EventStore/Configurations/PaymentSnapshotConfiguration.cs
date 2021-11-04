﻿using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using JsonNet.ContractResolvers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.EventSourcing.EventStore.Configurations;

public class PaymentSnapshotConfiguration : IEntityTypeConfiguration<PaymentSnapshot>
{
    public void Configure(EntityTypeBuilder<PaymentSnapshot> builder)
    {
        builder.HasKey(snapshot => new { snapshot.AggregateVersion, snapshot.AggregateId });

        builder
            .Property(snapshot => snapshot.AggregateVersion)
            .IsRequired();

        builder
            .Property(snapshot => snapshot.AggregateId)
            .IsRequired();

        builder
            .Property(snapshot => snapshot.AggregateName)
            .HasMaxLength(30)
            .IsUnicode(false)
            .IsRequired();

        builder
            .Property(snapshot => snapshot.AggregateState)
            .IsUnicode(false)
            .HasConversion(
                payment => JsonConvert.SerializeObject(payment),
                jsonString => JsonConvert.DeserializeObject<Payment>(jsonString,
                    new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() }))
            .IsRequired();
    }
}