using Microsoft.AspNetCore.Mvc;

namespace HydraWeb;

public class DashboardController : Controller
{

    [HttpGet("Dashboard")]
    public IActionResult Index(){
        return View("Index");
    }

}
