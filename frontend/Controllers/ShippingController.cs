using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frontend.Controllers
{
    public class ShippingController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
