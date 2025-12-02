using CManager.Application.ConsoleApp.Interface.Costumers;
using CManager.Application.ConsoleApp.Services.Costumers;
using CManager.Infrastructure.ConsoleApp.Repositories.Costumers;

CostumerService costumerService = new CostumerService(new CustomerRepository(@"c:\data\costumers.json"));

costumerService.CreateProfileAsync(new CManager.Domain.ConsoleApp.Models.Costumers.ProfileCreateRequest
{
    FirstName = "John",
    LastName = "Doe",
    PhoneNumber = "1234567890",
    Email = "hey@outlook.comn"
}).GetAwaiter().GetResult();

foreach (var profile in costumerService.GetAllProfilesAsync().GetAwaiter().GetResult())
{
    Console.WriteLine($"ID: {profile.Id}, Name: {profile.FirstName} {profile.LastName}, Email: {profile.Email}");
}

Console.ReadLine();