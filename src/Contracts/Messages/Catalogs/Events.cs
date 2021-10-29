using System;
using Messages.Abstractions.Events;

namespace Messages.Catalogs;

public static class Events
{
    public record CatalogCreated(Guid CatalogId, string Title) : Event;

    public record CatalogDeleted(Guid CatalogId) : Event;

    public record CatalogActivated(Guid CatalogId) : Event;

    public record CatalogDeactivated(Guid CatalogId) : Event;

    public record CatalogUpdated(Guid CatalogId, string Title) : Event;

    public record CatalogItemAdded(Guid CatalogId, string Name, string Description, decimal Price, string PictureUri) : Event;

    public record CatalogItemRemoved(Guid CatalogId, Guid CatalogItemId) : Event;

    public record CatalogItemUpdated(Guid CatalogId, Guid CatalogItemId, string Name, string Description, decimal Price, string PictureUri) : Event;
}