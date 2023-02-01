using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using acme_publishing_app.Models;
using acme_publishing_app.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace acme_publishing_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishController : ControllerBase
    {
        public readonly IDeliveryOrderService _deliveryOrderService;

        public PublishController(IDeliveryOrderService deliveryOrderService)
        {
            _deliveryOrderService = deliveryOrderService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<DeliveryOrder> deliveryOrders = await _deliveryOrderService.GetDeliveryOrders();

            for (int i = 0; i < deliveryOrders.Count; i++)
            {
                var baseUrl = deliveryOrders[i].PrintDistCompany.ApiUrl;
                var payload = new PublishPayload()
                {
                    SubscriptionName = deliveryOrders[i].Subscription.Name,
                    CustomerName = deliveryOrders[i].DeliveryAddress.Customer.FirstName + ' ' + deliveryOrders[i].DeliveryAddress.Customer.LastName,
                    CustomerAddress = deliveryOrders[i].DeliveryAddress.Description + ' ' + deliveryOrders[i].DeliveryAddress.Country.Name,
                };

                await CallExternalApi(baseUrl, payload);
            }
            return Ok();
        }

        private async Task<IActionResult> CallExternalApi(string baseUrl, PublishPayload payload)
        {
            var json = payload.ToString();
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                //Calling external web api
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsync(baseUrl, httpContent);
                var OrderResponse = new Object();
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response received from web api
                    var response = Res.Content.ReadAsStringAsync().Result;
                    OrderResponse = JsonConvert.DeserializeObject<Object>(response);
                }
                //returning the api response
                return Ok(OrderResponse);
            }
        }
    }
}