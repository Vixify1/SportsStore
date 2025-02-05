using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Models.ViewModels
{
    public class PagingInfo : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
