using API.Repositories.Interfaces;
using Shared.Models.DomainModels;
using System;
using System.Collections.Generic;
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

        public Task AddAsync(Settings settings)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
