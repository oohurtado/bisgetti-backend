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

        [HttpGet(template: "getBasics")]
        public async Task<ActionResult<PersonBasicsDTO>> GetBasics()
        {
            try
            {
                var role = User.FindFirstValue(System.Security.Claims.ClaimTypes.Role);
                var personId = int.Parse(User.FindFirstValue(Shared.Common.ClaimTypes.PersonId));

                var dto = await PersonRepository
                    .Get(p => p.Id == personId)
                    .Select(p => new PersonBasicsDTO()
                    {
                        Name = p.Name,
                        Role = role,
                    }).FirstOrDefaultAsync();

                return Ok(dto);
            }
            catch
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError));
            }
        }
    }
}
