using AutoMapper;
using infrastructure;
using infrastructure.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using orders.Models;
using orders.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orders.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class OrdersController: ControllerBase
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;

        //private readonly IPublishEndpoint _publishEP;
        private readonly ISendEndpointProvider _sendEPProvider;

        public OrdersController(
            IOrderRepository repo,
            IMapper mapper,
            //IPublishEndpoint publishEP, 
            ISendEndpointProvider sendEPProvider)
        {
            _repo = repo;
            _mapper = mapper;
            //_publishEP = publishEP;
            _sendEPProvider = sendEPProvider;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(Order order)
        {
            order.Status = Status.OrderGenerated;
            _repo.Create(order);
            await _repo.SaveChangesAsync();

            var sendEP = await _sendEPProvider.GetSendEndpoint(new Uri($"sb://{Constants.SB_HOST}/{Constants.SB_QUEUE_IC_REQ}"));
            await sendEP.Send<InventoryCheckRequestMessage>(_mapper.Map<InventoryCheckRequestMessage>(order));


            return Ok(order.Id);
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get()
        {
            return Ok(_repo.GetAll());
        }

        //[HttpPost("publish")]
        //public async Task<IActionResult> Publish(FlightOrder flightOrder)
        //{
        //    Console.WriteLine($"{flightOrder.FlightId} - {flightOrder.OrderId}");
        //    await _publishEP.Publish<FlightOrder>(flightOrder);

        //    return Ok();
        //}

        //[HttpPost("send")]
        //public async Task<IActionResult> Send(FlightCancellation flightCancellation)
        //{
        //    var sendEP = await _sendEPProvider.GetSendEndpoint(new Uri($"{Constants.SB_HOST}/{Constants.SB_QUEUE_FC}"));
        //    await sendEP.Send<FlightCancellation>(flightCancellation);

        //    return Ok();
        //}
    }
}
