using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Models.Common;
using Shared.Models.DomainModels;
using Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public IProductRepository ProductRepository { get; }
        public IMapper Mapper { get; }

        public ProductsController(
            IProductRepository productRepository,
            IMapper mapper
            )
        {
            ProductRepository = productRepository;
            Mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductCreateDTO dto)
        {
            try
            {
                var product = Mapper.Map<Product>(dto);

                await ProductRepository.AddAsync(product);
                await ProductRepository.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                SqlException e = ex.InnerException as SqlException;

                return e.Number switch
                {
                    2601 => BadRequest(new Response($"El product {dto.Name} ya existe", ex)),
                    _ => BadRequest(new Response(ResponseMessageType.UnknownError, ex)),
                };
            }
        }

        [HttpGet(template: "getPage/{column}/{order}/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<PageData<Product>>> GetPage(string column, string order, int pageNumber, int pageSize, string term = null)
        {
            try
            {
                // getting page
                var products = await ProductRepository
                    .GetByPage(column, order, pageNumber, pageSize, term, out int grandTotal)
                    .ToListAsync();

                var pageData = new PageData<Product>(products, grandTotal);
                return pageData;
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError, ex));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetOne(int id, bool withTies)
        {
            try
            {
                var product = await ProductRepository
                    .Get(p => p.Id == id, withTies)
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    return NotFound(new Response(ResponseMessageType.NotFound));
                }

                return product;
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError, ex));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductEditDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest(new Response(ResponseMessageType.BadRequest));

                var product = await ProductRepository
                    .Get(p => p.Id == id)
                    .FirstOrDefaultAsync();

                Mapper.Map(dto, product);
                ProductRepository.Update(product);
                await ProductRepository.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                SqlException e = ex.InnerException as SqlException;

                return e.Number switch
                {
                    2601 => BadRequest(new Response($"El product {dto.Name} ya existe", ex)),
                    _ => BadRequest(new Response(ResponseMessageType.UnknownError, ex)),
                };
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var product = await ProductRepository.Get(p => p.Id == id).FirstOrDefaultAsync();

                if (product == null)
                {
                    return NotFound(new Response(ResponseMessageType.NotFound));
                }
                else
                {
                    ProductRepository.Remove(product);
                    await ProductRepository.SaveAsync();

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
