using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectMVC.Controllers
{

    class JsonResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string access_token;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PaypalController : ControllerBase
    {

        private HttpClient http = new HttpClient();
        private static string baseurl = "https://api-m.sandbox.paypal.com";

        private readonly IConfiguration config;

        public PaypalController(IConfiguration config)
        {
            this.config = config;
        }


        [HttpGet]
        [Route("/paypal/createorder")]

        public async Task<IActionResult> CreateOrder ()
        {
            var token = await GenerateAccessToken();
            string url = $"{baseurl}/v2/checkout/orders";
            var body = new {
                intent = "CAPTURE",
                purchase_units = new []{
                    new {amount = new {currency_code = "USD", value = "100.00",}}
                }
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            //request.Headers.Add("content-type", "application/json");
            request.Headers.Add("Authorization", $"Bearer {token}");
            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            string res = await http.SendAsync(request).Result.Content.ReadAsStringAsync();
            var jsonres = JsonConvert.SerializeObject(res);
            return Ok(jsonres);
        }

        

        [HttpGet]
        [Route("/paypal/captureorder/{orderId:int}")]        
        public async Task<IActionResult> CaptureOrder([FromRoute]int orderId)
        {
            var token = await GenerateAccessToken();
            string url = $"{baseurl}/v2/checkout/orders/{orderId}/capture";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("content-type", "application/json");
            request.Headers.Add("Authorization", $"Bearer {token}");

            string res = await http.SendAsync(request).Result.Content.ReadAsStringAsync();
            var jsonres = JsonConvert.SerializeObject(res);
            return Ok(jsonres);
        }

        [HttpGet]
        [Route("paypal/GenerateAccessToken")]
        public async Task<IActionResult> GenerateAccessToken()
        {
            string idAndsecret = config.GetValue<string>("sb-clientID") + ":" + config.GetValue<string>("sb-secret");
            string auth = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(idAndsecret));

            string url = $"{baseurl}/v1/oauth2/token";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Authorization", $"Basic {auth}");
            request.Headers.Add("Accept", "application/json");
            request.Content = new StringContent("grant_type=client_credentials");
            string res = await http.SendAsync(request).Result.Content.ReadAsStringAsync();
            JsonResponse jsonObject = JsonConvert.DeserializeObject<JsonResponse>(res);
            return Content(jsonObject.access_token);
        }        
    }
}
