using infrastructure.Messages;
using MassTransit;
using orders.Models;
using orders.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orders.Consumers
{
    public class InventoryCheckResponseConsumer : IConsumer<InventoryCheckResponseMessage>
    {
        private readonly IOrderRepository _repo;

        public InventoryCheckResponseConsumer(
            IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task Consume(ConsumeContext<InventoryCheckResponseMessage> context)
        {
            Console.WriteLine($"Inventory Check Response: {context.Message.Succeeded}");

            var order = _repo.GetById(context.Message.OrderId);
            order.Status = context.Message.Succeeded ? Status.BillRequested : Status.InventoryCheckFailed;
            _repo.Update(order);
            await _repo.SaveChangesAsync();
        }
    }
}
