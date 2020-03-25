using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace frontend.Controllers
{
    public class OrdersController: BaseController
    {
        public OrdersController(IHttpClientFactory f) :
            base(f)
        {

        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "http://orders.52.170.169.207.nip.io/api/orders"
             );

            var response = await client.SendAsync(request);

            IList<OrderViewModel> data = null;
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<IList<OrderViewModel>>(result);
            }

            return View("Index", data);
        }
    }
}
