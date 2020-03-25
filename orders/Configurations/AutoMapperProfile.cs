using AutoMapper;
using infrastructure.Messages;
using orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orders.Configurations
{
    public class AutoMapperProfile: Profile 
    {
        public AutoMapperProfile()
        {
            IMappingExpression<Order, InventoryCheckRequestMessage> orderToOrderReceivedMessage =
                CreateMap<Order, InventoryCheckRequestMessage>()
                .ForMember(msg => msg.OrderId, opt => opt.MapFrom(order => order.Id));
        }
    }
}
