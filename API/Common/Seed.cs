using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shared;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Common
{
    public class Seed
    {
        public UserManager<ApplicationUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public IConfiguration Configuration { get; }

        public Seed(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            Configuration = configuration;
        }

        public async Task SeedAsync()
        {
            await CreateRolesAsync();
            await CreatePlaceAsync();

        }

        private async Task CreatePlaceAsync()
        {
            await Task.Delay(1);

            var placeName = Configuration["Place:Name"];
            var placeDescription = Configuration["Place:Description"];
            var placeEmails = Configuration["Place:Emails"];
            var placeKey = Configuration["Place:Key10"];

            if (string.IsNullOrEmpty(placeName.Trim()))
                throw new Exception("El campo placeName es nulo o esta vacio en el json");

            if (string.IsNullOrEmpty(placeDescription.Trim()))
                throw new Exception("El campo placeDescription es nulo o esta vacio en el json");

            if (string.IsNullOrEmpty(placeEmails.Trim()))
                throw new Exception("El campo placeEmails es nulo o esta vacio en el json");

            if (string.IsNullOrEmpty(placeKey.Trim()))
                throw new Exception("El campo placeKey es nulo o esta vacio en el json");


            // TODO: leer json y crear restaurante de ser necesario
            // TODO: leer json y crear usuarios en tabla persona con el rol admin
            // si placeKey no existe en tabla settings
                // crear en tabla settings
            // si placeKey si existe en tabla settings
                // checa si todos los emails existen en tabla persona
                    // si alguno no existe
                        // crear persona como admin                    


        }

        public async Task CreateRolesAsync()
        {
            var roles = Enum.GetValues(typeof(PersonType)).Cast<PersonType>();

            foreach (var role in roles)
            {
                bool exists = await RoleManager.RoleExistsAsync(role.GetPersonTypeName());
                if (!exists)
                {
                    await RoleManager.CreateAsync(new IdentityRole(role.GetPersonTypeRole()));
                }
            }
        }
    }
}
