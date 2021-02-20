using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RestaurantController : ControllerBase
    {
        public IProductRepository ProductRepository { get; }
        public ISettingsRepository SettingsRepository { get; }

        public RestaurantController(
            IProductRepository productRepository,
            ISettingsRepository settingsRepository
            )
        {
            ProductRepository = productRepository;
            SettingsRepository = settingsRepository;
        }

        [HttpPost(template: "generateMenu")]
        public async Task<ActionResult> GenerateMenu([FromBody] RestaurantGenerateMenuDTO dto)
        {
            try
            {                
                if (dto.GenerateProducts)
                {
                    var products = await ProductRepository.Get(p => p.IsHidden == false, false)
                        .ToListAsync();

                    var settings = await SettingsRepository.Get(p => true)
                        .FirstOrDefaultAsync();

                    settings.MenuProductsJson = JsonConvert.SerializeObject(products);
                    settings.MenuVersion = Guid.NewGuid().ToString();
                    await SettingsRepository.SaveAsync();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError, ex));
            }
        }

        [HttpGet(template: "getMenu")]
        public async Task<ActionResult<Menu>> GetMenu()
        {
            try
            {
                var settings = await SettingsRepository.Get(p => true)
                    .FirstOrDefaultAsync();

                List<Product> products = new List<Product>();
                if (!string.IsNullOrEmpty(settings.MenuProductsJson))
                {
                    products = JsonConvert.DeserializeObject<List<Product>>(settings.MenuProductsJson);
                }

                Menu menu = new Menu()
                {
                    Products = products
                };

                return menu;
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError, ex));
            }
        }

        [HttpDelete(template: "deleteMenu")]
        public async Task<ActionResult> DeleteMenu()
        {
            try
            {
                var settings = await SettingsRepository.Get(p => true)
                    .FirstOrDefaultAsync();

                settings.MenuProductsJson = null;
                settings.MenuVersion = null;

                await SettingsRepository.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError, ex));
            }
        }
    }
}
