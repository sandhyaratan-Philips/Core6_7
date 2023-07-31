using Autofac;
using Microsoft.AspNetCore.Mvc;
using ServiceContract;
using Services;

namespace AutofacExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILifetimeScope _scope;//IServiceScopeFactory
        private readonly ICitiesService _citiesService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILifetimeScope lifetimeScope, ICitiesService citiesService, IWebHostEnvironment webHostEnvironment)
        {
            _scope = lifetimeScope;
            _citiesService = citiesService;
            _webHostEnvironment = webHostEnvironment;

        }

        [Route("/")]
        public IActionResult Index()
        {
            List<string> cities = _citiesService.GetCities();
            ViewBag.Instance_CitiesService = _citiesService.ServiceInstanceId;
            using (ILifetimeScope scope = _scope.BeginLifetimeScope())
            {
                ICitiesService citiesService = scope.Resolve<ICitiesService>();
                ViewBag.Instance_CitiesService_Scoped = citiesService.ServiceInstanceId;
            }
            ViewBag.dev = _webHostEnvironment.IsDevelopment();
                return View(cities);
        }
        [Route("/Index1")]
        public IActionResult Index1()
        {
            return View();
        }

        [Route("/Index1")]
        public IActionResult other()
        {
            return View();
        }
    }
}
