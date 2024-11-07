using AutoMapper;
using ContactsApp.Database;
using ContactsApp.Database.Implementation;
using ContactsApp.Database.Interface;
using ContactsApp.ModelDto.MappingProfile;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Nexus.Idc.WebApi.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc(opts => opts.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "contact-app/dist";
});

//IConfiguration Configuration = new ConfigurationBuilder()
//                .SetBasePath(builder.Environment.ContentRootPath)
//                .AddJsonFile($"{builder.Environment.ContentRootPath}/appsettings.json", true, true).Build();

builder.Services.AddScoped<IDatabase>(context => new Database($"{AppContext.BaseDirectory}\\jsonDatabase.json"));
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped(_ =>
                 new MapperConfiguration(c =>
                 {
                     c.AddProfile<ContactProfile>();
                 }
                 ).CreateMapper());
var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseStaticFiles();
app.UseSpaStaticFiles();
app.UseRouting();

app.UseEndpoints(routes =>
{
    routes.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});


app.UseSpa(spa =>
{
    // To learn more about options for serving an Angular SPA from ASP.NET Core,
    // see https://go.microsoft.com/fwlink/?linkid=864501

    spa.Options.SourcePath = "contact-app";

    if (app.Environment.IsDevelopment())
    {
        spa.UseAngularCliServer(npmScript: "start");
        //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    }
});

app.Run();
