using ControllersExample.Controllers;

var builder = WebApplication.CreateBuilder(args);

//treat controller like servcie
//builder.Services.AddTransient<HomeController>();
builder.Services.AddControllers();
var app = builder.Build();
app.UseStaticFiles();
//app.MapGet("/", () => "Hello World!");

//enable routing
app.UseRouting();

app.MapControllers();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

app.Run();
