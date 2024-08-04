using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HMC_Project.Models;
using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Repositories;
using HMC_Project.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HMCDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("HMC")));

builder.Services.AddScoped<IRepDepartmentInterfaces, DepartmentRepository>();
builder.Services.AddScoped<IRepEmployeeintefaces, EmployeeReposity>();
builder.Services.AddScoped<IRepUserInterface, UserRepository>();

builder.Services.AddScoped<IDepartmentInterface, DepartmentServices>();
builder.Services.AddScoped<IEmployeeInterface, EmployeeServices>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HMC Project API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
