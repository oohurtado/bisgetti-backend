﻿using Shared.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    public interface ISettingsRepository
    {
        IQueryable<Settings> Get(Expression<Func<Settings, bool>> expression);
        Task AddAsync(Settings settings);
        Task<int> SaveAsync();
    }
}
