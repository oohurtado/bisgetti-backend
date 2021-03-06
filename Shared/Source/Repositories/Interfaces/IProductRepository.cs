﻿using Shared.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shared.Source.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<int> SaveAsync();
        public IQueryable<Product> GetByPage(string column, string order, int pageNumber, int pageSize, string term, out int grandTotal);
        IQueryable<Product> Get(Expression<Func<Product, bool>> expression, bool withTies = false);
        void Remove(Product product);
        void TrackChanges(Product product);
    }
}
