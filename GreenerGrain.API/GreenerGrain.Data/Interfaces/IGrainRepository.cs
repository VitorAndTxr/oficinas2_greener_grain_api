﻿using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Domain.Entities;

namespace GreenerGrain.Data.Interfaces
{
    public interface IGrainRepository : IRepositoryBase<Grain>
    {
    }
}