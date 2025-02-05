using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SportsStore.Models;
using System.Diagnostics;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ILogger<HomeController> _loggerService;

        private readonly IProductRepository repository;

        private readonly StoreSettings storeSettings;

        public HomeController(IProductRepository repo, IOptions<StoreSettings> settings)
        {
            repository = repo;
            storeSettings = settings.Value;
        }

        public ViewResult List()
        {
            ViewBag.StoreName = storeSettings.StoreName;
            ViewBag.WelcomeMessage = storeSettings.WelcomeMessage;
            ViewBag.TaxRate = storeSettings.TaxRate;
            return View(repository.Products);
        }

        //public IActionResult Error()
        //{
        //    return View();
        //}
        public IActionResult Index()
        {
            return View("List");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(/*new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }*/);
        }
    }
}
