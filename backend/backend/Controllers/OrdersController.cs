﻿using AutoMapper.Configuration;
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
    public class OrdersController : Controller
    {
        private OrdersService ordersService;

        private readonly IConfiguration _configuration;

        [HttpGet("test")]
        public IActionResult GetTest()
        {
            return Ok("It works!");
        }
        public OrdersController(IOrdersRepository ordersRepository, IConfiguration configuration)
        {
            this._configuration = configuration;
            ordersService = new OrdersService(ordersRepository);
        }

        [HttpPut("update-status")]
        public ActionResult<Boolean> UpdateOrderStatus([FromBody] OrderStatusDto orderStatus)
        {
            if (orderStatus == null)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);
            return Ok(ordersService.UpdateStatus(orderStatus));
        }

        [HttpGet("history/{id}")]

        public ActionResult<List<Order>> GetRecentOrder(int id)
        {
            return Ok(ordersService.GetRecentOrders(id));
        }
        [HttpPut]
        public ActionResult<Boolean> Save([FromBody] Order order)
        {
            return Ok();
        } 
    }
}
