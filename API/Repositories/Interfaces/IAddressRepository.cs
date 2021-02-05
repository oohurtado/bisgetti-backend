using Shared.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task AddAsync(Address address);
        Task<int> SaveAsync();
        IQueryable<Address> Get(Expression<Func<Address, bool>> expression, bool withTies = false);
        void Update(Address address);
        void Remove(Address address);
    }
}
