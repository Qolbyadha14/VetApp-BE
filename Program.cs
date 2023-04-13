using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using VetApp_BE.Config;
using VetApp_BE.Feature.Appointment.Repositories;
using VetApp_BE.Feature.Pets.Repositories;
using VetApp_BE.GenericRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Serilog
var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();


// Add Repository.
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();

// Database
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  
    options.AddServer(new OpenApiServer()
    {
        Url = "/",
    });

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title ="Vet App APi",
        Description = "An ASP.NET Core Web API for Service",
        Contact = new OpenApiContact
        {
            Name = "Vet Apps",
            Email = "VetApps@mail.com"
        },
        License = new OpenApiLicense
        {
            Name = "License By Vet Apps"
        }

    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
