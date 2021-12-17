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
        private IUserRepository userRepository;

        public OrdersService(IOrdersRepository ordersRepository, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.orderRepository = ordersRepository;
        }

        public List<Order> GetRecentOrders(String id)
        {
            Guid guid = new Guid(id);
            List<Order> userRecentOrders = new List<Order>();
            DateTime twoMountsAgo = DateTime.Now.AddDays(-60);
            foreach (Order order in orderRepository.LoadRelatedEntities())
            {
                if (order.OrderDate.Date > twoMountsAgo.Date && order.User.UserId.Equals(guid) && order.Status.Equals(OrderStatus.Delivered))
                    userRecentOrders.Add(order);
            }
            return userRecentOrders;
        }

        public List<Order> GetOrdersRequests()
        {
            List<Order> orders = new List<Order>();
            foreach (Order order in orderRepository.LoadRelatedEntities())
                if (order.Status.Equals(OrderStatus.PickUpRequest) || order.Status.Equals(OrderStatus.OrderRequest))
                    orders.Add(order);
            return orders;
        }

        public List<Order> UpdateStatus(Order order)
        {
            if (orderRepository.Update(order))
                return GetOrdersRequests();
            else throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
        }

        public object SaveReorder(Order order)
        {
            order.Order_Id = default(int);
            if (orderRepository.Reorder(order))
            {
                List<Order> orders = GetRecentOrders(order.User.UserId.ToString());
                return orders;
            }
            else
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}
