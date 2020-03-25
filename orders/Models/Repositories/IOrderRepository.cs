using infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orders.Models.Repositories
{
    public interface IOrderRepository: IRepositoryBase<Order>
    {
        Order GetById(int id);
    }
}
