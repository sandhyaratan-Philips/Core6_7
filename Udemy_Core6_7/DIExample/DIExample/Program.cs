
using ServiceContract;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//ioc container
//builder.Services.Add(new ServiceDescriptor(typeof(ICitiesService), //ServiceDescriptor tell about the service
//                                            typeof(CitiesService),
//                                            ServiceLifetime.Scoped));

builder.Services.AddScoped<ICitiesService, CitiesService>();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllers();

//app.MapGet("/", () => "Hello World!");

app.Run();

