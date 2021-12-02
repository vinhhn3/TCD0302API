using Microsoft.AspNetCore.Mvc;

namespace TCD0302.Web.Controllers
{
  public class ParksController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
