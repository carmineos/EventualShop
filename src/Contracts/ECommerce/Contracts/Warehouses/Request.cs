﻿using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Warehouses;

public static class Request
{
    public record ReceiveInventoryItem(Models.Product Product, int Quantity);

    public record IncreaseInventoryAdjust(int Quantity, string Reason);

    public record DecreaseInventoryAdjust(int Quantity, string Reason);
}