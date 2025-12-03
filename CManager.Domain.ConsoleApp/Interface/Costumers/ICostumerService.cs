using CManager.Domain.ConsoleApp.Models.Costumers;

namespace CManager.Domain.ConsoleApp.Interface.Costumers;

public interface ICostumerService
{
    // Create
    Task<bool> CreateProfileAsync(string firstName, string lastname, string email, string phonenumber, AddressInfo address);

    // Get
    Task<ProfileInfo> GetProfileAsync(string Id);
    Task<IReadOnlyList<ProfileInfo>> GetAllProfilesAsync();

    //Delete
    Task<bool> DeleteByEmail(string email);

}
