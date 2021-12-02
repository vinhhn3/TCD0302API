using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TCD0302.Web.Models;
using TCD0302.Web.Utils;

namespace TCD0302.Web.Controllers
{
  public class ParksController : Controller
  {
    private IHttpClientFactory _clientFactory;
    public ParksController(IHttpClientFactory clientFactory)
    {
      _clientFactory = clientFactory;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var client = _clientFactory.CreateClient("ParkService");

      var response = await client.GetAsync(
        ServiceUrl.ParkService);

      if (!response.IsSuccessStatusCode) return BadRequest("Something went wrong ...");

      // Convert json object to model
      var parks = JsonConvert.DeserializeObject<List<ParkViewModel>>(
        response.Content.ReadAsStringAsync().Result);

      return View(parks);
    }
    [HttpGet]
    public async Task<IActionResult> GetPark(int id)
    {
      var client = _clientFactory.CreateClient("ParkService");
      var response = await client
        .GetAsync($"{ServiceUrl.ParkService}/{id}");

      if (!response.IsSuccessStatusCode) return BadRequest("Something went wrong ...");

      var park = JsonConvert.DeserializeObject<ParkViewModel>(
        response.Content.ReadAsStringAsync().Result);

      return View(park);
    }

    [HttpGet]
    public IActionResult Create()
    {
      // Display the form
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(ParkViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      var client = _clientFactory.CreateClient("ParkService");
      HttpContent content = new StringContent(JsonConvert.SerializeObject(model),
        Encoding.UTF8, "application/json"
        );

      var response = await client
        .PostAsync(ServiceUrl.ParkService, content);

      if (!response.IsSuccessStatusCode) return BadRequest("Something went wrong ...");

      return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      var client = _clientFactory.CreateClient("ParkService");

      var response = await client
        .DeleteAsync($"{ServiceUrl.ParkService}/{id}");

      if (!response.IsSuccessStatusCode) return BadRequest("something went wrong ...");

      return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
      var client = _clientFactory.CreateClient("ParkService");
      var response = await client
        .GetAsync($"{ServiceUrl.ParkService}/{id}");

      if (!response.IsSuccessStatusCode) return BadRequest("Something went wrong ...");

      var park = JsonConvert.DeserializeObject<ParkViewModel>(
        response.Content.ReadAsStringAsync().Result);

      return View(park);
    }

    [HttpPost]
    public async Task<IActionResult> Update(ParkViewModel model)
    {
      if (!ModelState.IsValid) return View(model);

      var client = _clientFactory.CreateClient("ParkService");
      HttpContent content = new StringContent(JsonConvert.SerializeObject(model),
        Encoding.UTF8, "application/json"
        );

      var response = await client
        .PatchAsync($"{ServiceUrl.ParkService}/{model.Id}", content);

      if (!response.IsSuccessStatusCode) return BadRequest("Something went wrong ...");

      return RedirectToAction("Index");
    }

  }
}
