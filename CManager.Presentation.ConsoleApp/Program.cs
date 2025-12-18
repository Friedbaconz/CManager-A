using CManager.Application.ConsoleApp.Services.Costumers;
using CManager.Domain.ConsoleApp.Interface.Costumers;
using CManager.Domain.ConsoleApp.Models.Costumers;
using CManager.Infrastructure.ConsoleApp.Repositories.Costumers;
using CManager.Presentation.ConsoleApp.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = Host.CreateApplicationBuilder();

builder.Services.AddSingleton<ICostumerRepository>(new CustomerRepository(@"C:\Users\johyou\Desktop\data\costumers.json"));
builder.Services.AddSingleton<ICostumerService, CostumerService>();
builder.Services.AddSingleton<MenuControll>();

var app = builder.Build();

var menuController = app.Services.GetRequiredService<MenuControll>();
menuController.Run();

