using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureMapController : ControllerBase
    {
        [HttpGet("getGeoCode")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLocation(int empId)
        {
            var subscriptionKey = "MN84wEo1nrqpatQkVsnYlG1svQ9ZEw4IG6qU_6P82gE";
            var language = "en-US";
            var postalCode = "POI";
            var country = "USA";
            var query = "516 Alexander Rd, Princeton, NJ 08540, USA";
            string result = string.Empty;
            //GET CALL
            string apiURL = "https://atlas.microsoft.com/search/address/json?";
            UriBuilder builder = new UriBuilder(apiURL);
            builder.Query = "subscription-key=" + subscriptionKey + "&api-version=1.0&typeahead=true&language=" + language + "&extendedPostalCodesFor=" + postalCode + "&countrySet=" + country + "&query=" + query + "";

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(builder.Uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
            }
            return Ok(result);

        }



     

    }
}
