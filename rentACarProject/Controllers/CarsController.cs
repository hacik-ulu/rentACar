using Microsoft.AspNetCore.Mvc;

namespace rentACarProject.Controllers
{
    public class CarsController : Controller
    {
        
        public IActionResult Show()
        {
            return View();
        }
    }
}
