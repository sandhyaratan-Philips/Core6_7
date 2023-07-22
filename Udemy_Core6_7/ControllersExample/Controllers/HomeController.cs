using ControllersExample.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace ControllersExample.Controllers
{
    [Controller]
    public class Home:Controller
    {
        //http://localhost:5157/sayhello
        [Route("home")]
        [Route("/")]
        public ContentResult Index()
        {
            // return new ContentResult() { Content = "hello from index", ContentType = "text/plain" };
            return Content("hello from index", "text/plain");
        }

        [Route("about")]
        public string About()
        {
            return "hello from about";
        }

        [Route("person")]
        public JsonResult Person()
        {
            Person obj=new Person() { Id=Guid.NewGuid(),Name="SAN",Age=23};
            //return new JsonResult(obj);
            return Json(obj);
        }

        [Route("file-download")]
        public VirtualFileResult FileDownload()
        {
            //return new VirtualFileResult("/IMG_20220522_185212.jpg", "image/jpeg");
            return File("/IMG_20220522_185212.jpg", "image/jpeg");
        }


        [Route("file-download2")]
        public PhysicalFileResult FileDownload2()
        {
            // return new PhysicalFileResult(@"C:\core\file\20220311-EB-Designing_Event_Driven_Systems.pdf", "application/pdf");
            return PhysicalFile(@"C:\core\file\20220311-EB-Designing_Event_Driven_Systems.pdf", "application/pdf");
        }

        [Route("file-download3")]
        public FileContentResult FileDownload3()
        {
            byte[] arr = System.IO.File.ReadAllBytes(@"C:\core\file\design-pattern.pdf");
            return new FileContentResult(arr, "application/pdf");
        }

        //http://localhost:5157/file-download4?bookid=9
        [Route("file-download4")]
        public IActionResult FileDownload4()
        {
            if (!Request.Query.ContainsKey("bookId"))
            {
                //return new BadRequestResult();
                
               // return Content("not a valid book");
                return BadRequest("not a valid book");
            }
            else
            {
                byte[] arr = System.IO.File.ReadAllBytes(@"C:\core\file\design-pattern.pdf");
                return new FileContentResult(arr, "application/pdf");
            }
        }

        [Route("Book")]
        public IActionResult Book()
        {
            return RedirectToAction("Books", "Store");//302
        }

        [Route("Book2")]
        public IActionResult Book2()
        {
            return RedirectToActionPreserveMethod("Books", "Store");//307
        }

        [Route("Book3")]
        public IActionResult Book3()
        {
            return RedirectToActionPermanent("Books", "Store");//301
        }

        [Route("Book4")]
        public IActionResult Book4()
        {
            return LocalRedirect($"/store/Books");//301
        }

        [Route("Contact-us/{mobile:regex(^\\d{{10}}$)}")]
        public string Contact()
        {
            return "hello from contact us";
        }
    }
}
