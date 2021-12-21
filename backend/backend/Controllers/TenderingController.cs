using AutoMapper;
using backend.DTO;
using backend.DTO.TenderingDTO;
using backend.Model;
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
    public class TenderingController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly TenderingService tenderingService;
        private readonly IMapper _mapper;

        public TenderingController(IConfiguration configuration, IMapper mapper, TenderingService tenderingService)
        {
            this._mapper = mapper;
            this._configuration = configuration;
            this.tenderingService = tenderingService;
        }

        [HttpGet("test")]
        public IActionResult GetTest()
        {
            return Ok("It works!");
        }

        [HttpPost("request_a_tender_offer")]
        public ActionResult<TenderingOfferDTO> RequestTenderOffer ([FromBody] TenderingRequestDTO tenderingRequestDTO)
        {
            if(tenderingRequestDTO == null || tenderingRequestDTO.requestedItems.Count == 0)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);
            }
            TenderingOffer tenderingOffer = tenderingService.RequestTenderOfffer(tenderingRequestDTO);
            if(tenderingOffer == null)
            {
                throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.ExpectationFailed);
            }
            TenderingOfferDTO tenderingOfferDTO = _mapper.Map<TenderingOfferDTO>(tenderingOffer);
            return Ok(tenderingOfferDTO);
        }



        /* [HttpPost("update_quantity")]
         public ActionResult<ShoppingCartFrontDTO> UpdateItemQuantityInCart([FromBody] UpdateShoppingCartsItemQuantityDTO updateShoppingCartsItemQuantityDTO)
         {
             Boolean successfullyUpdated = false;
             if (updateShoppingCartsItemQuantityDTO == null || updateShoppingCartsItemQuantityDTO.newQuantity < 0)
             {
                 throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);
             }

             successfullyUpdated = shoppingCartService.UpdateItemQuantityInCart(updateShoppingCartsItemQuantityDTO);

             if (!successfullyUpdated)
                 throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);
             else
             {
                 ShoppingCart updatedSC = shoppingCartService.GetById(updateShoppingCartsItemQuantityDTO.ShoppingCarts_Id);
                 ShoppingCartFrontDTO scTransformed = _mapper.Map<ShoppingCartFrontDTO>(updatedSC);

                 return Ok(scTransformed);


         */

    }
        }
