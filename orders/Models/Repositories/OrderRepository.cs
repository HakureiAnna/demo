using infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orders.Models.Repositories
{
    public class OrderRepository: RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrdersDbContext context):
            base(context)
        {
        }

        public Order GetById(int id)
        {
            return GetByCondition(o => o.Id == id).First();
        }
    }
}
