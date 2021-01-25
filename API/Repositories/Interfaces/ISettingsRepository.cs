using Shared.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    public interface ISettingsRepository
    {
        Task AddAsync(Settings settings);
        Task<int> SaveAsync();
    }
}
