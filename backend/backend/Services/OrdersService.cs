using backend.DTO;
using backend.Model;
using backend.Model.Enum;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace backend.Services
{
    public class OrdersService
    {
        private IOrdersRepository orderRepository;

        public OrdersService(IOrdersRepository ordersRepository)
        {
            this.orderRepository = ordersRepository;
        }

        public List<Order> GetRecentOrders(int userId)
        {
            List<Order> userRecentOrders = new List<Order>();
            DateTime twoMountsAgo = DateTime.Now.AddDays(-60);
            foreach (Order order in orderRepository.GetAll())
                if (order.User.UserId.Equals(userId) && order.OrderDate > twoMountsAgo && order.Status.Equals(OrderStatus.Delivered))
                    userRecentOrders.Add(order);
            return userRecentOrders;

        }

        public List<Order> GetOrdersRequests()
        {
            List<Order> orders = new List<Order>();
            foreach (Order order in orderRepository.GetAll())
                if (order.Status.Equals(OrderStatus.PickUpRequest) || order.Status.Equals(OrderStatus.OrderRequest))
                    orders.Add(order);
            return orders;
        }

        internal Boolean UpdateStatus(OrderStatusDto orderStatus)
        {
            Order order=orderRepository.GetById(orderStatus.Order_id);
            if(order == null)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            order.Status = orderStatus.OrderStatus;
            return orderRepository.Update(order);
        }

        internal object UpdateReorder(Order order)
        {
            if (orderRepository.Update(order))
            {
                List<Order> orders = GetOrdersRequests();
                return orders;
            }
            else
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}
