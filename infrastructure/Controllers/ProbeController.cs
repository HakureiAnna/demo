using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Controllers
{
    public class ProbeController: ControllerBase
    {
        [Route("/healthz")]
        [HttpGet]
        public string healthz()
        {
            return "OK";
        }
    }
}
