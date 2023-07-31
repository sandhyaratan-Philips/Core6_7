using Autofac;
using Autofac.Extensions.DependencyInjection;
using ServiceContract;
using Services;

var builder = WebApplication.CreateBuilder(args);
//autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());//using 3rd part Service Provider Factory; repersentation of IOC container


//app.MapGet("/", () => "Hello World!");

builder.Services.AddControllersWithViews();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    //containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerDependency(); //addtranisent
    containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerLifetimeScope(); // addscoped
    //containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().SingleInstance(); // addsingletone
});

var app = builder.Build();

if (app.Environment.IsDevelopment()|| app.Environment.IsEnvironment("QA"))
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllers();



app.Run();
