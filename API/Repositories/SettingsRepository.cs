using API.Repositories.Interfaces;
using Shared.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        public DatabaseContext Context { get; }

        public SettingsRepository(DatabaseContext context)
        {
            Context = context;
        }

        public IQueryable<Settings> Get(Expression<Func<Settings, bool>> expression)
        {
            return Context.Settings.Where(expression);
        }

        public async Task AddAsync(Settings settings)
        {
            await Context.AddAsync(settings);
        }

        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
