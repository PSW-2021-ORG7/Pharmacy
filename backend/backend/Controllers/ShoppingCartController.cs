using AutoMapper.Configuration;
using backend.DTO;
using backend.Repositories.Interfaces;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : Controller
    {
        private ShoppingCartService shoppingCartService;

        private readonly IConfiguration _configuration;

        public ShoppingCartController(IShoppingCardsRepository shoppingCardsRepository, IConfiguration configuration)
        {
            this._configuration = configuration;
            this.shoppingCartService = new ShoppingCartService(shoppingCardsRepository);
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



    }
}
