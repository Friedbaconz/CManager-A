using CManager.Domain.ConsoleApp.Models.Costumers;
namespace CManager.Application.ConsoleApp.Interface.Costumers;

public interface ICostumerRepository
{
    // Add
    Task <bool> AddProfileRange(IEnumerable<ProfileInfo> profile);

    // Get
    Task<IEnumerable<ProfileInfo>> ProfileByAll();

    // Delete 
    bool DeleteProfileById(string id);
}