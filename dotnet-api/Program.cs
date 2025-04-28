using dotnet_api.Services.Interfaces;
using dotnet_api.Services;
using dotnet_api.Mapping;
using System.Reflection;
using dotnet_api.Data;
using Microsoft.EntityFrameworkCore;
using Python.Runtime;


var builder = WebApplication.CreateBuilder(args);

// 🔥 Khởi tạo Python runtime tại đây
//PythonEngine.Initialize();
//AppDomain.CurrentDomain.ProcessExit += (_, __) => PythonEngine.Shutdown();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 👇 Thêm Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Register other services
builder.Services.AddScoped<IConstructionService, ConstructionService>();
builder.Services.AddScoped<WeatherPredictionService>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
