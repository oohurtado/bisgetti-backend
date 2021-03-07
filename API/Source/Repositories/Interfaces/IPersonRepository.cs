using Shared.Models.DomainModels;
using Shared.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.Source.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        Task AddRangeAsync(string emails, PersonType personType);
        IQueryable<Person> Get(Expression<Func<Person, bool>> expression);
        Task AddAsync(Person person);
        Task<int> SaveAsync();
        void Update(Person person);
    }
}
