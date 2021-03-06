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
using Shared.Models.Jsons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class SettingsController : ControllerBase
    {
        public IProductRepository ProductRepository { get; }
        public ISettingsRepository SettingsRepository { get; }

        public SettingsController(
            IProductRepository productRepository,
            ISettingsRepository settingsRepository
            )
        {
            ProductRepository = productRepository;
            SettingsRepository = settingsRepository;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(template: "createMenu")]
        public async Task<ActionResult> CreateMenu([FromBody] SettingsGenerateMenuDTO dto)
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet(template: "getMenuTitles")]
        public async Task<ActionResult<SettingsMenuTitlesDTO>> GetMenuTitles()
        {
            try
            {
                var settings = await SettingsRepository.Get(p => true)
                    .FirstOrDefaultAsync();

                SettingsMenuTitlesDTO model = new SettingsMenuTitlesDTO();
                MenuTitlesJson menuTitlesJson = new MenuTitlesJson();
                if (!string.IsNullOrEmpty(settings.MenuMessagesJson))
                {
                    menuTitlesJson = JsonConvert.DeserializeObject<MenuTitlesJson>(settings.MenuMessagesJson);

                    model.FirstTitle = menuTitlesJson.FirstTitle;
                    model.SecondTitle = menuTitlesJson.SecondTitle;
                }

                return model;

            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError, ex));
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut(template: "updateMenuTitles")]
        public async Task<ActionResult> UpdateMenuTitles([FromBody] SettingsMenuTitlesDTO dto)
        {
            try
            {
                var settings = await SettingsRepository.Get(p => true)
                    .FirstOrDefaultAsync();

                MenuTitlesJson menuTitlesJson = new MenuTitlesJson()
                {
                    FirstTitle = dto.FirstTitle,
                    SecondTitle = dto.SecondTitle
                };

                settings.MenuMessagesJson = JsonConvert.SerializeObject(menuTitlesJson);
                await SettingsRepository.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError, ex));
            }
        }

        [HttpGet(template: "getPlaceInformation")]
        public async Task<ActionResult<SettingsPlaceInformationDTO>> GetPlaceInformation()
        {
            try
            {                
                var settings = await SettingsRepository.Get(p => true)
                    .FirstOrDefaultAsync();

                SettingsPlaceInformationDTO model = new SettingsPlaceInformationDTO();
                PlaceInformationJson placeInformationJson = new PlaceInformationJson();
                if (!string.IsNullOrEmpty(settings.PlaceInformationJson))
                {
                    placeInformationJson = JsonConvert.DeserializeObject<PlaceInformationJson>(settings.PlaceInformationJson);

                    model.Address = placeInformationJson.Address;
                    model.Emails = placeInformationJson.Emails;
                    model.OpenHours = placeInformationJson.OpenHours;
                    model.Phones = placeInformationJson.Phones;
                }

                return model;

            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError, ex));
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut(template: "updatePlaceInformation")]
        public async Task<ActionResult> UpdatePlaceInformation([FromBody] PlaceInformationJson dto)
        {
            try
            {
                var settings = await SettingsRepository.Get(p => true)
                    .FirstOrDefaultAsync();

                PlaceInformationJson placeInformationJson = new PlaceInformationJson()
                {
                    Address = dto.Address,
                    Emails = dto.Emails,
                    OpenHours = dto.OpenHours,
                    Phones = dto.Phones,
                };

                settings.PlaceInformationJson = JsonConvert.SerializeObject(placeInformationJson);
                await SettingsRepository.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError, ex));
            }
        }

        [HttpGet(template: "getOnlineOptions")]
        public async Task<ActionResult<SettingsOnlineOptionsDTO>> GetOnlineOptions()
        {
            try
            {
                var settings = await SettingsRepository.Get(p => true)
                    .FirstOrDefaultAsync();

                SettingsOnlineOptionsDTO model = new SettingsOnlineOptionsDTO()
                {
                    IsOnlineActive = settings.IsOnlineActive,
                    HasHomeDelivery = settings.HasHomeDelivery,
                    ShippingCost = settings.ShippingCost,
                };

                return model;

            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ResponseMessageType.UnknownError, ex));
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut(template: "updateOnlineOptions")]
        public async Task<ActionResult> UpdateOnlineOptions([FromBody] SettingsOnlineOptionsDTO dto)
        {
            try
            {
                var settings = await SettingsRepository.Get(p => true)
                    .FirstOrDefaultAsync();

                settings.IsOnlineActive = dto.IsOnlineActive;
                settings.HasHomeDelivery = dto.HasHomeDelivery;
                settings.ShippingCost = dto.ShippingCost;

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
