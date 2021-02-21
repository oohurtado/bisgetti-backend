using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Models.Common;
using Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonsController : ControllerBase
    {
        public IPersonRepository PersonRepository { get; }

        public PersonsController(IPersonRepository personRepository)
        {
            PersonRepository = personRepository;
        }

        [HttpGet(template: "getRole")]
        public ActionResult<PersonRoleDTO> GetRole()
        {
            try
            {
                var role = User.FindFirstValue(System.Security.Claims.ClaimTypes.Role);

                var dto = new PersonRoleDTO()
                {
                    Role = role,
                };

                return dto;
            }
            catch
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError));
            }
        }

        [HttpGet(template: "getName")]
        public async Task<ActionResult<PersonNameDTO>> GetName()
        {
            try
            {
                var personId = int.Parse(User.FindFirstValue(Shared.Common.ClaimTypes.PersonId));

                var dto = await PersonRepository
                    .Get(p => p.Id == personId)
                    .Select(p => new PersonNameDTO()
                    {
                        Name = p.Name,
                    }).FirstOrDefaultAsync();

                dto.Name = GetFixedName(dto.Name);

                return dto;
            }
            catch
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError));
            }
        }

        private static string GetFixedName(string name)
        {
            var arr = name.Split(' ');

            if (arr.Length == 1 || arr.Length == 2)
            {
                return name;
            }
            else if (arr.Length == 3)
            {
                return $"{arr[0]} {arr[1]}";
            }
            else if (arr.Length == 4)
            {
                return $"{arr[0]} {arr[2]}";
            }
            else if (arr.Length >= 5)
            {
                return $"{arr[0]} {arr[1]}";
            }
            else
            {
                return arr[0];
            }
        }
    }
}
