using Microsoft.AspNetCore.Mvc;
using ServiceContract;
using Services;

namespace DIExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICitiesService _citiesService1;
        private readonly ICitiesService _citiesService2;
        private readonly ICitiesService _citiesService3;

        public HomeController(ICitiesService citiesService1, ICitiesService citiesService2, ICitiesService citiesService3)
        {
            _citiesService1 = citiesService1;//object from IOC container
            _citiesService2 = citiesService2;//object from IOC container
            _citiesService3 = citiesService3;//object from IOC container
        }

        //[Route("/")]
        //public IActionResult Index([FromServices] ICitiesService citiesService)
        //{
        //    List<string> cities= citiesService.GetCities();//_citiesService.GetCities();

        //    return View(cities);
        //}
        [Route("/")]
        public IActionResult Index()
        {
            List<string> cities = _citiesService1.GetCities();
            ViewBag.Instance_CitiesService_1 = _citiesService1.ServiceInstanceId;
            ViewBag.Instance_CitiesService_2 = _citiesService2.ServiceInstanceId;
            ViewBag.Instance_CitiesService_3 = _citiesService3.ServiceInstanceId;
            return View(cities);
        }
    }
}
