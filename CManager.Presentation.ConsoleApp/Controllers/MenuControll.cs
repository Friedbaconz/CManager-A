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

        //showlist

        var profiles = _costumerService.GetAllProfilesAsync().GetAwaiter().GetResult();
        if(profiles.Result != null)
        foreach (var profile in profiles.Result)
        {
            Console.WriteLine($"ID: {profile.Id}, Name: {profile.FirstName} {profile.LastName}, Email: {profile.Email}, Phone: {profile.PhoneNumber}");
        }

        //Show single profile

        string email = "Emails@domain.com";

        var singleProfile = _costumerService.GetByEmail(email).GetAwaiter().GetResult();
        if(singleProfile.Result != null)
        {
            var prof = singleProfile.Result;

            Console.WriteLine($"Single Profile: {prof.Id}, Name: {prof.FirstName} {prof.LastName}, Email: {prof.Email}, Phone: {prof.PhoneNumber}");
            
        }

        Console.ReadLine();
    }
}
