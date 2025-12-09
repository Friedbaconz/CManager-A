using CManager.Domain.ConsoleApp.Interface.Costumers;
using CManager.Application.ConsoleApp.Services.Costumers;
using CManager.Domain.ConsoleApp.Models.Costumers;
using CManager.Infrastructure.ConsoleApp.Repositories.Costumers;

ICostumerRepository icostumerrepository = new CustomerRepository(@"C:\Users\johyou\Desktop\data\costumers.json");
ICostumerService costumerService = new CostumerService(icostumerrepository);

string option = string.Empty;
bool consoleSwitch = true;

do
{
    switch (option)
    {
        case "1":
            do
            {
                Console.Clear();
                option = string.Empty;
                Console.WriteLine("2 = Make profile | 1 = Exit");
                option = Console.ReadLine()!;
                if (option == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Name");
                    string FirstName = Console.ReadLine()!;

                    if (FirstName != null)
                    {
                        Console.Clear();
                        Console.WriteLine("LastName");
                        string LastName = Console.ReadLine()!;

                        if (LastName != null)
                        {
                            Console.Clear();
                            Console.WriteLine("Email");
                            string Email = Console.ReadLine()!;

                            if (Email != null)
                            {
                                Console.Clear();
                                Console.WriteLine("PhoneNumber");
                                string PhoneNumber = Console.ReadLine()!;

                                if (PhoneNumber != null)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Adress");
                                    string ort = Console.ReadLine()!;
                                    Console.WriteLine("StreetName");
                                    string Street = Console.ReadLine()!;
                                    Console.WriteLine("PostNumber");
                                    string Post = Console.ReadLine()!;

                                    if (Street != null)
                                    {
                                        await costumerService.CreateProfileAsync(
                                            firstName: FirstName,
                                            lastname: LastName,
                                            email: Email,
                                            phonenumber: PhoneNumber,
                                            address: new AddressInfo
                                            {
                                                Ort = ort,
                                                StreetName = Street,
                                                PostNumbers = Post
                                            }

                                        );

                                        Console.WriteLine(FirstName + " was added as a profile");
                                        Console.ReadKey();
                                    }
                                }
                            }
                        }
                    }
                }
                if ( option == "1")
                {
                    break;
                }
            }
            while (option == "2");
            break;

        case "2":
            Console.Clear();
            option = string.Empty;
            foreach (var profile in await costumerService.GetAllProfilesAsync())
            {
                Console.WriteLine($"ID: {profile.Id}, Name: {profile.FirstName} {profile.LastName}, Email: {profile.Email}, Address:(Ort: {profile.AddressProfile.Ort} Street:{profile.AddressProfile.StreetName} PostNumber:{profile.AddressProfile.PostNumbers}");
                Console.WriteLine("");
            }
            Console.ReadKey();
            break;

        case "3":
            Console.Clear();
            option = string.Empty;
            string id = Console.ReadLine()!;
            break;

        default:
            Console.Clear();
            option = string.Empty;
            Console.WriteLine("1 : Add Users");
            Console.WriteLine("2 : Show Users");
            Console.WriteLine("3 : Show A User");
            Console.WriteLine("4 : Remove A User");
            option = Console.ReadLine()!;
            break;
    }
}
while (consoleSwitch);