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

        [Route("Contact-us/{mobile:regex(^\\d{{10}}$)}")]
        public string Contact()
        {
            return "hello from contact us";
        }
    }
}
