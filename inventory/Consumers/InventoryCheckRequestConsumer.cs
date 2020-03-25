using infrastructure;
using infrastructure.Messages;
using inventory.Models.Repositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.Consumers
{
    public class InventoryCheckRequestConsumer : IConsumer<InventoryCheckRequestMessage>
    {
        private readonly IProductRepository _repo;
        private readonly ISendEndpointProvider _sendEPProvider;

        public InventoryCheckRequestConsumer(
            IProductRepository repo,
            ISendEndpointProvider sendEPProvider)
        {
            _repo = repo;
            _sendEPProvider = sendEPProvider;
        }

        public async Task Consume(ConsumeContext<InventoryCheckRequestMessage> context)
        {
            Console.WriteLine($"Inventory Check Request Received: OrderId: {context.Message.OrderId}");

            var product = _repo.GetById(context.Message.ProductId);

            var response = new InventoryCheckResponseMessage
            {
                ProductId = context.Message.ProductId,
                OrderId = context.Message.OrderId,
                Succeeded = false                       // default to fail
            };
            var sendEP = await _sendEPProvider.GetSendEndpoint(
                new Uri($"sb://{Constants.SB_HOST}/{Constants.SB_QUEUE_IC_RES}"));

            if (product != null && product.Quantity >= context.Message.Quantity)
            {
                product.Quantity -= context.Message.Quantity;
                _repo.Update(product);
                await _repo.SaveChangesAsync();
                response.Succeeded = true;
                await sendEP.Send<InventoryCheckResponseMessage>(response);
            } else
            {
                await sendEP.Send<InventoryCheckResponseMessage>(response);
            }
        }
    }
}
