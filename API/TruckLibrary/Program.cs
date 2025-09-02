using System;
using TruckLibrary.Core.DataContext;
using Microsoft.EntityFrameworkCore;
using TruckLibrary.Domain.Repositories;
using TruckLibrary.Core.Repositories;
using TruckLibrary.Domain.Services;
using TruckLibrary.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database config
builder.Services.AddDbContext<TruckLibraryDbContext>(options =>
        options.UseSqlite("Data Source=meubanco.db", b => b.MigrationsAssembly("TruckLibrary.Core")));

builder.Services.AddScoped<ITruckRepository, TruckRepository>();
builder.Services.AddScoped<ITruckService, TruckService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
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

app.UseCors();

app.Run();
