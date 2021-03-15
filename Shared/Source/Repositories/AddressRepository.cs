using Shared.Models.DomainModels;
using Shared.Source;
using Shared.Source.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shared.Source.Repositories
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

        public IQueryable<Address> GetByPage(string column, string order, int pageNumber, int pageSize, string term, out int grandTotal)
        {
            IQueryable<Address> addresses;
            IOrderedQueryable<Address> ioq = null;

            var iq = Context.Addresses.AsQueryable();

            if (!String.IsNullOrEmpty(term))
                iq = iq.Where(p => p.Name.Contains(term));

            grandTotal = iq.Count();

            if (column == "name" && order == "asc")
                ioq = iq.OrderBy(p => p.Name);
            else if (column == "name" && order == "desc")
                ioq = iq.OrderByDescending(p => p.Name);

            addresses = ioq
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return addresses;
        }
    }
}
