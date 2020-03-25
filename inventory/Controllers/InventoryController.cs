using infrastructure.Controllers;
using inventory.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class InventoryController: ControllerBase
    {
        private readonly IProductRepository _repo;

        public InventoryController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get()
        {
            return Ok(_repo.GetAll());
        }
    }
}
