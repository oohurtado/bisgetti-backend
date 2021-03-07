using API.Source.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Common;
using Shared.Models.DomainModels;
using Shared.Models.DTOs;
using Shared.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AddressesController : ControllerBase
    {
        public IAddressRepository AddressRepository { get; }
        public IMapper Mapper { get; }

        public AddressesController(
            IAddressRepository addressRepository,
            IMapper mapper
            )
        {
            AddressRepository = addressRepository;
            Mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AddressCreateDTO dto)
        {
            try
            {
                var val = User.FindFirstValue(Shared.Source.ClaimTypes.PersonId);
                var personId = int.Parse(val);

                var address = Mapper.Map<Address>(dto);
                address.PersonId = personId;

                await AddressRepository.AddAsync(address);
                await AddressRepository.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                SqlException e = ex.InnerException as SqlException;

                return e.Number switch
                {
                    2601 => BadRequest(new Response($"La dirección {dto.Name} ya existe", ex)),
                    _ => BadRequest(new Response(ResponseMessageType.UnknownError, ex)),
                };
            }
        }

        [HttpGet(template: "getPage/{column}/{order}/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<PageData<Address>>> GetPage(string column, string order, int pageNumber, int pageSize, string term = null)
        {
            try
            {
                // getting page
                var addresses = await AddressRepository
                    .GetByPage(column, order, pageNumber, pageSize, term, out int grandTotal)
                    .ToListAsync();

                var pageData = new PageData<Address>(addresses, grandTotal);
                return pageData;
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError, ex));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetOne(int id, bool withTies)
        {
            try
            {
                var val = User.FindFirstValue(Shared.Source.ClaimTypes.PersonId);
                var personId = int.Parse(val);

                var address = await AddressRepository
                    .Get(p => p.Id == id && p.PersonId == personId, withTies)
                    .FirstOrDefaultAsync();

                if (address == null)
                {
                    return NotFound(new Response(ResponseMessageType.NotFound));
                }

                return address;
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError, ex));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AddressEditDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest(new Response(ResponseMessageType.BadRequest));

                var val = User.FindFirstValue(Shared.Source.ClaimTypes.PersonId);
                var personId = int.Parse(val);

                var address = await AddressRepository
                    .Get(p => p.Id == id && p.PersonId == personId)
                    .FirstOrDefaultAsync();

                Mapper.Map(dto, address);
                AddressRepository.Update(address);
                await AddressRepository.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                SqlException e = ex.InnerException as SqlException;

                return e.Number switch
                {
                    2601 => BadRequest(new Response($"La dirección {dto.Name} ya existe", ex)),
                    _ => BadRequest(new Response(ResponseMessageType.UnknownError, ex)),
                };
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var val = User.FindFirstValue(Shared.Source.ClaimTypes.PersonId);
                var personId = int.Parse(val);

                var address = await AddressRepository
                    .Get(p => p.Id == id && p.PersonId == personId)
                    .FirstOrDefaultAsync();

                if (address == null)
                {
                    return NotFound(new Response(ResponseMessageType.NotFound));
                }
                else
                {
                    AddressRepository.Remove(address);
                    await AddressRepository.SaveAsync();

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError, ex));
            }
        }
    }
}
