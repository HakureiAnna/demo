using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace frontend.Controllers
{
    public class BaseController: Controller
    {
        protected readonly IHttpClientFactory _clientFactory;

        public BaseController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
