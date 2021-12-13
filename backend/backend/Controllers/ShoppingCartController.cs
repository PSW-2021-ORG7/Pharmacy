using AutoMapper;
using AutoMapper.Configuration;
using backend.DTO;
using backend.Model;
using backend.Repositories.Interfaces;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;


namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ShoppingCartService shoppingCartService;

        public ShoppingCartController(IConfiguration configuration, IMapper mapper, ShoppingCartService shoppingCartService)
        {
            this._configuration = configuration;
            this._mapper = mapper;
            this.shoppingCartService = shoppingCartService;
            
        }

        [HttpGet("test")]
        public IActionResult GetTest()
        {
            return Ok("It works!");
        }


        [HttpPost("update_quantity")]
        public ActionResult<Boolean>  UpdateItemQuantityInCart([FromBody] UpdateShoppingCartsItemQuantityDTO updateShoppingCartsItemQuantityDTO)
        {
            Boolean successfullyUpdated = false;
            if(updateShoppingCartsItemQuantityDTO == null || updateShoppingCartsItemQuantityDTO.newQuantity<0)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                successfullyUpdated = shoppingCartService.UpdateItemQuantityInCart(updateShoppingCartsItemQuantityDTO);
                return Ok(successfullyUpdated);
            }
        }

        [HttpGet("{user_id?}")]
        public ActionResult<ShoppingCart> GetByUser(Guid user_id)
        {
            ShoppingCart sc =  shoppingCartService.GetByUser(user_id);
            if (sc == null)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            }
            return Ok(sc);
        }


    }
}
