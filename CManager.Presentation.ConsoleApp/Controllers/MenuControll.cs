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
        string input = string.Empty;
        bool menu = true;
        do {
            switch (input) {

                case "1":
                    //create profile
                    bool on = true;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Enter First Name:");
                        string firstName = Console.ReadLine();

                        Console.Clear();
                        Console.WriteLine("Enter Last Name:");
                        string lastName = Console.ReadLine();

                        Console.Clear();
                        Console.WriteLine("Enter Email:");
                        string emails = Console.ReadLine();

                        Console.Clear();
                        Console.WriteLine("Enter PhoneNumber:");
                        string phoneNumber = Console.ReadLine();

                        if (phoneNumber != null || emails != null || firstName != null || lastName != null)
                        {
                            on = false;
                            var request = new ProfileCreateRequest
                            {

                                FirstName = firstName,

                                LastName = lastName,

                                Email = emails,

                                PhoneNumber = phoneNumber,


                            };

                            _costumerService.CreateProfileAsync(request);

                        }
                        else                         
                        {
                            Console.WriteLine("Invalid input. Please try again.");
                        }
                    } while (on);

                    Console.WriteLine("Profile Created Successfully!");

                    input = string.Empty;
                    break;


            case "2":
                    //showlist
                    Console.Clear();
                    
                    var profiles = _costumerService.GetAllProfiles().GetAwaiter().GetResult();
                    if (profiles.Result != null)
                    {
                        foreach (var profile in profiles.Result)
                        {
                            Console.WriteLine($"ID: {profile.Id}, Name: {profile.FirstName} {profile.LastName}, Email: {profile.Email}, Phone: {profile.PhoneNumber}");
                        }
                    }
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    input = string.Empty;
                    break;

            case "3":
                    //show single profile by email
                    Console.Clear();
                    Console.WriteLine("Enter Email to search:");
                    string searchEmail = Console.ReadLine();
                    var singleProfile = _costumerService.GetByEmail(searchEmail).GetAwaiter().GetResult();
                    if (singleProfile.Result != null)
                    {
                        var prof = singleProfile.Result;
                        Console.WriteLine($"Profile Found: {prof.Id}, Name: {prof.FirstName} {prof.LastName}, Email: {prof.Email}, Phone: {prof.PhoneNumber}");
                    }
                    else
                    {
                        Console.WriteLine("Profile not found.");
                    }
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    input = string.Empty;
                    break;


            case "4":
                    //remove profile
                    Console.Clear();
                    Console.WriteLine("Remove profile by email");
                    string RemoveEmail = Console.ReadLine();
                    var profilegone = _costumerService.GetByEmail(RemoveEmail).GetAwaiter().GetResult();
                    if (profilegone.Result != null)
                    {
                        var removeprofile = _costumerService.DeleteByEmail(RemoveEmail).GetAwaiter().GetResult();
                        Console.WriteLine("profile was removed");
                    }
                    else
                    {
                        Console.WriteLine("profile wasn't found");
                    }
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    input = string.Empty;
                    break;


                default:
                    Console.Clear();
                    input = string.Empty;
                    Console.WriteLine("Menu Options:");
                    Console.WriteLine("1. Create Profile");
                    Console.WriteLine("2. View All Profiles");
                    Console.WriteLine("3. View Profile by Email");
                    Console.WriteLine("4. Remove Profile by Email");
                    Console.WriteLine("5. Exit");
                    Console.Write("Select an option: ");
                    input = Console.ReadLine();
                    if (input == "5")
                    {
                        Console.WriteLine("Exiting...");

                        return;
                    }
                    break;
            }









        } while (menu);

        Console.ReadLine();
    }
}
