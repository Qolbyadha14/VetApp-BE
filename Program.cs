using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using VetApp_BE.Config;
using VetApp_BE.Feature.Appointment.Models;
using VetApp_BE.Feature.Appointment.Repositories;
using VetApp_BE.Feature.Pets.Repositories;
using VetApp_BE.GenericRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod() // This will allow any HTTP method.
               .AllowAnyHeader() // This will allow any header.
               .WithExposedHeaders("Content-Disposition"); // This will expose the "Content-Disposition" header.
    });
});


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
/*    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dbContext.Database.Migrate();

    var dummyAppointments = AppointmentDummyData.GenerateAppointments(50);
    dbContext.Appointments.AddRange(dummyAppointments);
    dbContext.SaveChanges();*/

    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
