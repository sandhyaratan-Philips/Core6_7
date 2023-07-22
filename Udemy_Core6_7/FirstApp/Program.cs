//var builder = WebApplication.CreateBuilder(args);<-default
//without wwwroot CreateBuilder throw exception
using FirstApp.CustomConstraints;
using FirstApp.CustomMiddleware;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    WebRootPath = "myroot"
});

//add custome middle ware
builder.Services.AddTransient<MyCustomMiddleware>();
//add constraint for route
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthsCustomConstraints));//months is user defined constraints
});

var app = builder.Build();

//get static files from configured wwwroot folder
app.UseStaticFiles();//work with myroot
app.UseStaticFiles(new StaticFileOptions()//for MyWebRoot
{
    FileProvider = new PhysicalFileProvider(
       Path.Combine(builder.Environment.ContentRootPath, "MyWebRoot")
        )
});

/*
 ////Middleware 
 
app.MapGet("/", () => "Hello World!"); //short cut to make url map

do api call using run()
app.Run(async (HttpContext context) =>
{
    //GET
    //string path=context.Request.Path;
    //context.Response.Headers["content-type"] = "text/html";
    //if (context.Request.Method == "GET")
    //{//http://localhost:5224/?id=123
    //    if (context.Request.Query.ContainsKey("id"))
    //    {
    //        string id=context.Request.Query["id"];
    //        await context.Response.WriteAsync($"<p>{path}:{context.Request.Method}</p>");
    //        await context.Response.WriteAsync($"<p>{id}</p>");
    //    }
    //}

    //POST
    //StreamReader reader = new StreamReader(context.Request.Body);
    //string body=await reader.ReadToEndAsync();
    //Dictionary<string,StringValues> dic= Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body); //give key value pair of body

    //if (dic.ContainsKey("name"))
    //{
    //    string name = dic["name"];
    //    await context.Response.WriteAsync(name);
    //}

});

chain of middle ware

middleware0
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync(" \n middleware0 ");
    await next(context);
});
middleware1
app.UseMiddleware<MyCustomMiddleware>();-> can add like this
app.UseMyCustomMiddleware();//extension method
middleware2
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync(" \n middleware2 ");
    await next(context);
});
*/
/*
app.UseAuthMiddleware();

app.UseWhen(context => context.Request.Query.ContainsKey("id"),
   app => {
       app.Use(async (context, next) =>
       {
           await context.Response.WriteAsync($"\n hello id ");
           await next(context);
       });
   });

app.UseMyConventionalMiddleware();
*/
////routing///

//enable routing
app.UseRouting();
// always write after UseRouting
app.Use(async (context, next) =>
{
    Endpoint? endpoint = context.GetEndpoint();
    if (endpoint != null)
        await context.Response.WriteAsync($"endpoint: {endpoint}\n");
    await next(context);
});

//create endpoints
app.UseEndpoints(endpoints =>
{
    //add endpoints

    //default 
    endpoints.Map("/", async context =>
    {
        await context.Response.WriteAsync("hello \n");
    });


    endpoints.Map("file/{Filename}.{extention:minlength(2)?}", async (context) =>
    {//extention is optional n of typr string
        string? filename = Convert.ToString(context.Request.RouteValues["filename"]);
        await context.Response.WriteAsync($"in /file {filename}");
    });

    endpoints.Map("emp/{name:length(4,7):alpha=harsha}", async (context) =>
    { //harsha is default parameter n need alphabet
        string? name = Convert.ToString(context.Request.RouteValues["name"]);
        await context.Response.WriteAsync($"in /emp {name}");
    });

    //convert below route to customconstarints
    //endpoints.Map("sales/{year:int:min(1990)}/{month:regex(^(apr|jul|june)$)}", async (context) =>
    endpoints.Map("sales/{year:int:min(1990)}/{month:months}", async (context) =>
    { //sales/2020/apr
        string? year = Convert.ToString(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);
        if (month != null)
            await context.Response.WriteAsync($"in sales: {year}/{month}");
        else
            await context.Response.WriteAsync($"{month} is not for sales");
    });
    /*
    http://localhost:5224/map1
    it will support GET, POST
    endpoints.Map("map1", async (context) =>
    {
        await context.Response.WriteAsync("in Map1");
    });
    http://localhost:5224/map2
    endpoints.MapPost("map2", async (context) =>
    {
        await context.Response.WriteAsync("in Map2");
    });
    endpoints.MapGet("map3", async (context) =>
    {
        await context.Response.WriteAsync("in Map3");
    });
    */

    //sales/2024/jan<- if we pass 2024 then it has higher prefrence that sales/{year}/{month} as above
    endpoints.Map("sales/2024/jan", async context =>
    {
        await context.Response.WriteAsync("with literal text");
    });
});

//middleware3
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync(" \n middleware3 ");
    //used to termenating/short circuiting middleware that doesnt forward the request
});


app.Run();
