using Dapr;
using OrdersApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdersApp.Services
{
    public class OrderService
    {
        private readonly StateClient _stateClient;

        public OrderService(StateClient stateClient)
        {
            _stateClient = stateClient;
        }

        public async Task<User> GetUserWithOrders(Guid userId)
        {
            var state = await _stateClient.GetStateEntryAsync<User>(userId.ToString());

            return state.Value;
        }

        public async Task CreateOrder(Guid userId, string orderData)
        {
            var state = await _stateClient.GetStateEntryAsync<User>(userId.ToString());
            state.Value ??= new User
            {
                Id = userId,
                Orders = new List<Order>()
            };
            state.Value.Orders.Add(new Order
            {
                OrderId = Guid.NewGuid(),
                OrderData = orderData
            });

            await state.SaveAsync();
        }
    }
}
