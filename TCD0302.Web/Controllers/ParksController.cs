using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TCD0302.Web.Models;

namespace TCD0302.Web.Controllers
{
  public class ParksController : Controller
  {
    private IHttpClientFactory _clientFactory;
    public ParksController(IHttpClientFactory clientFactory)
    {
      _clientFactory = clientFactory;
    }
    public async Task<IActionResult> Index()
    {
      var client = _clientFactory.CreateClient("ParkService");

      var response = await client.GetAsync(
        "api/parks");

      if (!response.IsSuccessStatusCode) return BadRequest("Something went wrong ...");

      // Convert json object to model
      var parks = JsonConvert.DeserializeObject<List<ParkViewModel>>(
        response.Content.ReadAsStringAsync().Result);

      return View(parks);
    }
  }
}
