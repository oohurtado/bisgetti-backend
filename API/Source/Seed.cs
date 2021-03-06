﻿using Shared.Source.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shared;
using Shared.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models;

namespace API.Source
{
    public class Seed
    {
        public UserManager<ApplicationUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public IConfiguration Configuration { get; }
        public IPersonRepository PersonRepository { get; }
        public ISettingsRepository SettingsRepository { get; }

        public Seed(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IPersonRepository personRepository,
            ISettingsRepository settingsRepository)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            Configuration = configuration;
            PersonRepository = personRepository;
            SettingsRepository = settingsRepository;
        }

        public async Task SeedAsync()
        {
            await CreateRolesAsync();
            await CreatePeopleAsync(PersonType.Owner);
            await CreateSettingsAsync();
        }

        private async Task CreateSettingsAsync()
        {
            await SettingsRepository.CreateAsync();
            await SettingsRepository.SaveAsync();
        }

        private async Task CreatePeopleAsync(PersonType personType)
        {
            var emails = Configuration["Emails"];

            if (emails == null)
                return;

            await PersonRepository.AddRangeAsync(emails, personType);
            await PersonRepository.SaveAsync();
        }

        public async Task CreateRolesAsync()
        {
            var roles = Enum.GetValues(typeof(PersonType)).Cast<PersonType>();

            foreach (var role in roles)
            {
                bool exists = await RoleManager.RoleExistsAsync(role.GetPersonTypeRole());
                if (!exists)
                {
                    await RoleManager.CreateAsync(new IdentityRole(role.GetPersonTypeRole()));
                }
            }
        }
    }
}
