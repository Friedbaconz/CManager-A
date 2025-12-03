using CManager.Domain.ConsoleApp.Interface.Costumers;
using CManager.Application.ConsoleApp.Services.Costumers;
using CManager.Domain.ConsoleApp.Models.Costumers;
using CManager.Infrastructure.ConsoleApp.Repositories.Costumers;

ICostumerRepository icostumerrepository = new CustomerRepository(@"C:\Users\johyou\Desktop\data\costumers.json");
ICostumerService costumerService = new CostumerService(icostumerrepository);



await costumerService.CreateProfileAsync(
    firstName: "John",
    lastname: "Doe",
    email: "email@outlook.com",
    phonenumber: "1234567890",
    address: new AddressInfo
    {
        Ort = "Stockholm",
        StreetName = "Main Street 1",
        PostNumbers = "12345"
    }

    );


foreach (var profile in await costumerService.GetAllProfilesAsync())
{
    Console.WriteLine($"ID: {profile.Id}, Name: {profile.FirstName} {profile.LastName}, Email: {profile.Email}, Address:(Ort: {profile.AddressProfile.Ort} Street:{profile.AddressProfile.StreetName} PostNumber:{profile.AddressProfile.PostNumbers}");
}

Console.ReadLine();