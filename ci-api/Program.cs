using CIManagement.Entities;
using CIManagement.Helpers;
using CIManagement.Interfaces;
using CIManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var _appSettings = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(_appSettings);

var cicnxnString = builder.Configuration.GetSection("AppSettings")["CIMConnection"];

builder.Services.AddDbContext<CI_Context>(options => options.UseMySql(cicnxnString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql")));

// DI for Services
builder.Services.AddScoped<IStaffInterface, StaffService>();
builder.Services.AddScoped<ICustomerInterface, CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
