using CManager.Application.ConsoleApp.Services.Costumers;
using CManager.Domain.ConsoleApp.Factory;
using CManager.Domain.ConsoleApp.Interface.Costumers;
using CManager.Domain.ConsoleApp.Models.Costumers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
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
                    bool on = true;
                    string firstName = string.Empty;
                    string lastName = string.Empty;
                    string emails = string.Empty;
                    string phoneNumber = string.Empty;
                    string Ort = string.Empty;
                    string streetName = string.Empty;
                    string PostNumbers = string.Empty;
                    do
                    {
                        Console.Clear();
                        if (string.IsNullOrEmpty(firstName))
                        {
                            Console.Clear();
                            Console.WriteLine("Enter First Name:");
                            firstName = Console.ReadLine();
                        }

                        if (string.IsNullOrEmpty(lastName))
                        {
                            Console.Clear();
                            Console.WriteLine("Enter Last Name:");
                            lastName = Console.ReadLine();
                        }

                        if (string.IsNullOrEmpty(emails))
                        {
                            Console.Clear();
                            Console.WriteLine("Enter Email:");
                            emails = Console.ReadLine();
                        }

                        if (string.IsNullOrEmpty(phoneNumber))
                        {
                            Console.Clear();
                            Console.WriteLine("Enter PhoneNumber:");
                            phoneNumber = Console.ReadLine();
                        }

                        if(string.IsNullOrEmpty(Ort))
                        {
                            Console.Clear();
                            Console.WriteLine("Enter Ort:");
                            Ort = Console.ReadLine();
                        }

                        if (string.IsNullOrEmpty(PostNumbers))
                        {
                            Console.Clear();
                            Console.WriteLine("Enter PostNumber:");
                            PostNumbers = Console.ReadLine();
                        }

                        if (string.IsNullOrEmpty(streetName))
                        {
                            Console.Clear();
                            Console.WriteLine("Enter StreetName:");
                            streetName = Console.ReadLine();
                        }

                        if (!string.IsNullOrEmpty(emails) || !string.IsNullOrEmpty(phoneNumber) || !string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName) || !string.IsNullOrEmpty(Ort) || !string.IsNullOrEmpty(streetName) || !string.IsNullOrEmpty(PostNumbers))
                        {
                            on = false;
                            var request = new ProfileCreateRequest
                            {

                                FirstName = firstName,

                                LastName = lastName,

                                Email = emails,

                                PhoneNumber = phoneNumber,

                                Address = new AddressInfo
                                {
                                    Ort = Ort,
                                    PostNumbers = PostNumbers,
                                    StreetName = streetName
                                }

                            };



                            var result = _costumerService.CreateProfileAsync(request).GetAwaiter().GetResult();
                            string reason = result.Message;
                            if (result.Success)
                            {
                                Console.Clear();
                                Console.WriteLine("Profile " + firstName + " creation succeeded.");
                                Console.WriteLine("Back to menu = empty | make another profile = 2");
                                string option = Console.ReadLine();
                                if (option == string.Empty)
                                {
                                    on = false;
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Profile creation failed.");
                                Console.WriteLine("Reason is: " + reason);
                                Console.WriteLine(" ");
                                Console.WriteLine("Back to menu = empty | renter profile = 2");
                                on = true;
                                string option = Console.ReadLine();
                                if (option == string.Empty)
                                {
                                    on = false;
                                }
                                if (option == "2")
                                {
                                    
                                    if (reason == "Profile with this email already exists." || result.Message == "Invalid Email")
                                    {
                                        request.Email = string.Empty;
                                        emails = string.Empty;
                                        
                                    }

                                    if (reason == "Invalid First Name")
                                    {
                                        request.FirstName = string.Empty;
                                        firstName = string.Empty;
                                        
                                    }

                                    if (reason == "Invalid Last Name")
                                    {
                                        request.LastName = string.Empty;
                                        lastName = string.Empty;
                                        
                                    }

                                    if (reason == "Invalid PhoneNumber")
                                    {
                                        request.PhoneNumber = string.Empty;
                                        phoneNumber = string.Empty;
                                        
                                    }

                                    if (reason == "Invalid PostNumber")
                                    {
                                        request.Address.PostNumbers = string.Empty;
                                        PostNumbers = string.Empty;

                                    }

                                    if (reason == "Invalid Ort")
                                    {
                                        request.Address.Ort = string.Empty;
                                        Ort = string.Empty;

                                    }

                                    if (reason == "Invalid StreetName")
                                    {
                                        request.Address.StreetName = string.Empty;
                                        streetName = string.Empty;

                                    }
                                }
                            }
                        }
                        else                         
                        {
                            Console.WriteLine("Invalid input. Please try again.");
                            Console.WriteLine("Back to menu = empty | renter profile = 2");
                            string option = Console.ReadLine();
                            if (option == string.Empty)
                            {
                                on = false;
                            }

                        }
                    } while (on);

                    input = string.Empty;
                    break;


            case "2":
                    Console.Clear();
                    
                    var profiles = _costumerService.GetAllProfiles().GetAwaiter().GetResult();
                    if (profiles.Result != null)
                    {
                        foreach (var profile in profiles.Result)
                        {
                            Console.WriteLine($"Name: {profile.FirstName} {profile.LastName}, Email: {profile.Email}");
                        }
                    }
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    input = string.Empty;
                    break;

            case "3":
                    Console.Clear();
                    Console.WriteLine("Enter Email to search:");
                    string searchEmail = Console.ReadLine();
                    var singleProfile = _costumerService.GetByEmail(searchEmail).GetAwaiter().GetResult();
                    if (singleProfile.Result != null)
                    {
                        var prof = singleProfile.Result;
                        Console.WriteLine($"Profile Found: {prof.FirstName}");
                        Console.WriteLine($"Id: {prof.Id}, Name: {prof.FirstName} {prof.LastName}, Email: {prof.Email}, Phone: {prof.PhoneNumber}");
                        Console.WriteLine($"Ort: {prof.Address.Ort}, PostNumber: {prof.Address.PostNumbers}, StreetName: {prof.Address.StreetName}");
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
