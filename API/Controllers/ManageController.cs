using API.Source;
using API.Models;
using API.Source.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Models.Common;
using Shared.Models.DomainModels;
using Shared.Models.DTOs;
using Shared.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageController : ControllerBase
    {
        public IPersonRepository PersonRepository { get; }
        public UserManager<ApplicationUser> UserManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }
        public IConfiguration Configuration { get; }

        public ManageController(
            IPersonRepository personRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            PersonRepository = personRepository;
            UserManager = userManager;
            SignInManager = signInManager;
            Configuration = configuration;
        }


        [HttpPost(template: "signUp")] // .../api/manage/signUp
        public async Task<ActionResult<UserToken>> SignUp([FromBody] ManageSignUpDTO dto)
        {
            try
            {
                ApplicationUser applicationUser = new ApplicationUser
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                };

                var result = await UserManager.CreateAsync(applicationUser, dto.Password);

                if (result.Succeeded)
                {
                    var user = await UserManager.FindByEmailAsync(dto.Email);
                    var person = await PersonRepository.Get(p => p.Email == dto.Email).FirstOrDefaultAsync();

                    if (person == null)
                    {
                        person = new Person()
                        {
                            IsRegistered = true,
                            IsVerified = false,
                            CreationTime = DateTime.Now,
                            Email = dto.Email,
                            Name = dto.Name,
                            PersonType = PersonType.Client,
                        };

                        await PersonRepository.AddAsync(person);
                        await PersonRepository.SaveAsync();
                    }
                    else
                    {
                        person.CreationTime = DateTime.Now;
                        person.IsRegistered = true;
                        person.IsVerified = true;
                        person.Name = dto.Name;

                        await PersonRepository.SaveAsync();
                    }

                    await UserManager.AddToRoleAsync(user, person.PersonType.GetPersonTypeRole());
                    
                    var claims = CustomToken.GetClaims(user.Id, person.Id, person.Email, person.PersonType.GetPersonTypeRole());
                    var token = CustomToken.BuildToken(claims, Configuration["JWT:Key"]);
                    return token;
                }
                else
                {
                    return BadRequest(new Response(ResponseMessageType.AlreadyExist));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError, ex));
            }
        }

        [HttpPost(template: "logIn")] // .../api/manage/logIn
        public async Task<ActionResult<UserToken>> LogIn([FromBody] ManageLogInDTO dto)
        {
            try
            {
                var result = await SignInManager.PasswordSignInAsync(dto.Email, dto.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await UserManager.FindByEmailAsync(dto.Email);
                    var roles = await UserManager.GetRolesAsync(user);

                    var person = await PersonRepository
                        .Get(p => p.Email == dto.Email)
                        .FirstOrDefaultAsync();

                    var claims = CustomToken.GetClaims(user.Id, person.Id, person.Email, roles.FirstOrDefault());
                    var token = CustomToken.BuildToken(claims, Configuration["JWT:Key"]);
                    return token;
                }
                else
                {
                    return BadRequest(new Response(ResponseMessageType.WrongCredentials));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError, ex));
            }
        }
    }
}
