using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        public DatabaseContext Context { get; }

        public AddressRepository(DatabaseContext context)
        {
            Context = context;
        }

        public async Task AddAsync(Address address)
        {
            await Context.AddAsync(address);
        }

        public IQueryable<Address> Get(Expression<Func<Address, bool>> expression, bool withTies = false)
        {
            var iq = Context.Addresses
                .Where(expression);

            if (withTies)
            {
            }

            return iq;
        }

        public void Remove(Address address)
        {
            Context.Addresses.Remove(address);
        }

        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Update(Address address)
        {
            Context.Entry(address).State = EntityState.Modified;
        }
    }
}
