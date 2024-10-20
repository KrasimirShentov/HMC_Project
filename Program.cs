using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HMC_Project.Models;
using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Repositories;
using HMC_Project.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HMCDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("HMC")));

builder.Services.AddScoped<IRepDepartmentInterfaces, DepartmentRepository>();
builder.Services.AddScoped<IRepEmployeeintefaces, EmployeeReposity>();
builder.Services.AddScoped<IRepTrainingInterface, TrainingRepository>();
builder.Services.AddScoped<IRepUserInterface, UserRepository>();

builder.Services.AddScoped<IDepartmentInterface, DepartmentServices>();
builder.Services.AddScoped<IEmployeeInterface, EmployeeServices>();
builder.Services.AddScoped<ITrainingInterface, TrainingService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//    };
//});


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
