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
    public class OrdersController : Controller
    {
        private OrdersService ordersService;

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        [HttpGet("test")]
        public IActionResult GetTest()
        {
            return Ok("It works!");
        }
        public OrdersController(IOrdersRepository ordersRepository, IMapper mapper, IUserRepository userRepository, IConfiguration configuration)
        {
            _mapper = mapper;
            this._configuration = configuration;
            ordersService = new OrdersService(ordersRepository,userRepository);
        }

        [HttpPut("update-status")]
        public ActionResult<List<Order>> UpdateOrderStatus([FromBody] OrderWithIdDTO dto)
        {
            if (dto == null)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);
            Order order = _mapper.Map<Order>(dto);
            order.User.UserId = new Guid(dto.UserId);
            return Ok(ordersService.UpdateStatus(order));
        }

        [HttpGet("history/{id}")]

        public ActionResult<List<Order>> GetRecentOrder(String id)
        {
            List<Order> orders = ordersService.GetRecentOrders(id);
            return Ok(orders);
        }
        [HttpPost]
        public ActionResult<Boolean> Save([FromBody] Order order)
        {
            return Ok();
        }
        [HttpGet("requests")]
        public ActionResult<List<Order>> GetRequests()
        {
            return Ok(ordersService.GetOrdersRequests());
        }

        [HttpPost("update-reorder")]
        public ActionResult<List<Order>> UpdateReorder([FromBody] OrderDTO dto)
        {
            if (dto == null)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);
            Order order = _mapper.Map<Order>(dto);
            order.User.UserId = new Guid(dto.UserId);

            return Ok(ordersService.SaveReorder(order));
        }
    }
}
