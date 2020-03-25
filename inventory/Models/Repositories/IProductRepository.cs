using infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.Models.Repositories
{
    public interface IProductRepository: IRepositoryBase<Product>
    {
        Product GetById(int id);
    }

}
