using ControllersExample.CustomModelBinder;
using ControllersExample.Model;
using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    [Controller]
    public class Store : Controller
    {
        [Route("store/books")]
        public IActionResult Books()
        {
            return Content("welcome");
        }
        //http://localhost:5157/books?id=1
        [Route("books")]
        public IActionResult BookWithId([FromQuery]int id)
        {
            return Content("Query param book id: " +Convert.ToString(Request.Query["id"]));
        }

        //http://localhost:5157/books/1
        [Route("books/{id?}")]//? make it null able
        public IActionResult BookWithIdRoute([FromRoute] int? id)
        {
            return Content("Route param book id: " +id);
        }

        //http://localhost:5157/emp/2?name=san
        [Route("emp/{id?}")]//? make it null able
        public IActionResult EmpWithIdRoute(emp obj)
        {
            if(!ModelState.IsValid)
            { //http://localhost:5157/emp/
                return BadRequest("enter proper data");
            }
            return Content("Route param emp id: " + obj.id+ " emp name: "+obj.name);
        }
        [Route("emp")]//? make it null able
        public IActionResult Emp([FromBody] [ModelBinder(BinderType = typeof(NameModelBinder))] emp obj)
        {
            if (!ModelState.IsValid)
            { //http://localhost:5157/emp/
                return BadRequest("enter proper data");
            }
            return Content("Route param emp id: " + obj.id + " emp name: " + obj.name);
        }

        [Route("emp")]//? make it null able
        public IActionResult Index([FromHeader(Name ="User-Agent")] string userAgent)
        {
           
            return Content("header comtent for user agent: " + userAgent);
        }

    }
}
