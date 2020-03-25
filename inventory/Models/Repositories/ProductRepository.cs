using infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace inventory.Models.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(InventoryDbContext context)
            : base(context)
        {
        }

        public Product GetById(int id)
        {
            return GetByCondition(p => p.Id == id).First();
        }
    }
}
