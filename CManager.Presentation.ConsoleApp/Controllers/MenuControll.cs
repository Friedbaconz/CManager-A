using CManager.Application.ConsoleApp.Services.Costumers;
using CManager.Domain.ConsoleApp.Interface.Costumers;
using CManager.Domain.ConsoleApp.Models.Costumers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Presentation.ConsoleApp.Controllers;

public sealed class MenuControll(ICostumerService costumerService)
{
    private readonly ICostumerService _costumerService = costumerService;
    
    public void Run()
    {
        Console.WriteLine("Menu Controller is running");



        var request = new ProfileCreateRequest
        {
            FirstName = "bob",
            LastName = "Joe",
            Email = "Emails@domain.com",
            PhoneNumber = "123465789",

        };

        _costumerService.CreateProfileAsync(request);

        Console.ReadLine();
    }
}
