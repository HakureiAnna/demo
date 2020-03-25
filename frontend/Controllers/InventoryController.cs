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
    public class InventoryController: BaseController
    {
        public InventoryController(IHttpClientFactory f):
            base(f)
        {

        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "http://inventory.52.170.169.207.nip.io/api/inventory"
             );

            var response = await client.SendAsync(request);

            IList<ProductViewModel> data = null;
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<IList<ProductViewModel>>(result);
            }

            return View("Index", data);
        }
    }
}
