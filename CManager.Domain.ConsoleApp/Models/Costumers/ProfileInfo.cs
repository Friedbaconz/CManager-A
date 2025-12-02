using CManager.Domain.ConsoleApp.Models.Costumers;

namespace CManager.Domain.ConsoleApp.Models.Costumers;

public class ProfileInfo
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Id { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public AdressInfo AdressProfile { get; set; } = new AdressInfo();

}

public class ProfileCreateRequest : ProfileInfo
{
    
}
