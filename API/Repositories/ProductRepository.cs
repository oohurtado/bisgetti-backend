using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public DatabaseContext Context { get; }

        public ProductRepository(DatabaseContext context)
        {
            Context = context;
        }

        public async Task AddAsync(Product product)
        {
            await Context.AddAsync(product);
        }

        public IQueryable<Product> Get(Expression<Func<Product, bool>> expression, bool withTies = false)
        {
            var iq = Context.Products
                .Where(expression);

            if (withTies)
            {                
            }

            return iq;
        }

        public IQueryable<Product> GetByPage(string column, string order, int pageNumber, int pageSize, string term, out int grandTotal)
        {
            IQueryable<Product> products;
            IOrderedQueryable<Product> ioq = null;

            var iq = Context.Products.AsQueryable();

            if (!String.IsNullOrEmpty(term))
                iq = iq.Where(p => p.Name.Contains(term));

            grandTotal = iq.Count();

            if (column == "name" && order == "asc")
                ioq = iq.OrderBy(p => p.Name);
            else if (column == "name" && order == "desc")
                ioq = iq.OrderByDescending(p => p.Name);
            else if (column == "price" && order == "asc")
                ioq = iq.OrderBy(p => p.Price);
            else if (column == "price" && order == "desc")
                ioq = iq.OrderByDescending(p => p.Price);

            products = ioq
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return products;
        }

        public void Remove(Product product)
        {
            Context.Products.Remove(product);
        }

        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Update(Product product)
        {
            Context.Entry(product).State = EntityState.Modified;
        }
    }
}
