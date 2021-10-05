﻿using System;
using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections
{
    public record CartDetailsProjection :  IProjection
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int Age { get; init; }
        public bool IsDeleted { get; init; }
    }
}