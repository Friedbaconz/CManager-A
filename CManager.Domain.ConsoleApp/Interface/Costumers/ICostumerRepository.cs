using CManager.Domain.ConsoleApp.Models.Costumers;
namespace CManager.Domain.ConsoleApp.Interface.Costumers;

public interface ICostumerRepository
{
    // Add
    Task <bool> AddProfileRangeAsync(IEnumerable<ProfileInfo> profile);

    // Get
    Task<IEnumerable<ProfileInfo>> ProfileByAllAsync();

    Task<IEnumerable<ProfileInfo>> ProfileByEmailAsync(string id);

    // Delete 
    Task<bool> DeleteProfileById(string id);

}