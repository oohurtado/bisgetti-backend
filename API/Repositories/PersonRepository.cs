using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public DatabaseContext Context { get; }

        public PersonRepository(DatabaseContext context)
        {
            Context = context;
        }

        public async Task AddRangeAsync(string emails, PersonType personType)
        {
            var emailsToAttemptToAdd = emails.Split(",");
            var emailsFromDatabase = await Context.People.Select(p => p.Email).ToListAsync();
            var emailsToAdd = emailsToAttemptToAdd.Except(emailsFromDatabase).ToList();

            List<Person> people = new List<Person>();

            foreach (var email in emailsToAdd)
            {
                Person person = new Person()
                {
                    Email = email,
                    PersonType = personType,
                    Registered = false,
                    Verified = false,
                    CreationTime = DateTime.Now,
                };

                people.Add(person);
            }

            await Context.People.AddRangeAsync(people);
        }

        public IQueryable<Person> Get(Expression<Func<Person, bool>> expression)
        {
            return Context.People.Where(expression);
        }

        public async Task AddAsync(Person person)
        {
            await Context.AddAsync(person);
        }

        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Update(Person person)
        {
            Context.Entry(person).State = EntityState.Modified;
        }
    }
}
