using Application.AutoMapperProfiles;
using Application.Commands.UserCommands;
using Application.Handlers.ProductHandlers;
using Application.Handlers.UserHandlers;
using Application.Queries.ProductQueries;
using Application.Queries.UserQueries;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using FluentValidation;
using Application.Validators;
using MediatR;
using Application.Behaviors;
using API.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()) 
    .AddJsonFile(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Presentation", "Config", "appsettings.json"), optional: false, reloadOnChange: true);

builder.Services.AddAutoMapper(typeof(UserProfile));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

builder.Services.AddValidatorsFromAssembly(typeof(UserCreateValidator).Assembly);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(UserController).GetTypeInfo().Assembly);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
