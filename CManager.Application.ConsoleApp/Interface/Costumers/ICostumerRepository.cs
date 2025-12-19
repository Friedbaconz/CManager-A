using CManager.Domain.ConsoleApp.Models.Costumers;
namespace CManager.Domain.ConsoleApp.Interface.Costumers;

public interface ICostumerRepository
{
    // Add

    Task<ProfileResult> Add(ProfileInfo profile);
    Task<ProfileResult> AddRangeAsync(IEnumerable<ProfileInfo> profile);

    // Get
    Task<ObjectResult<IEnumerable<ProfileInfo>?>> GetAsync(Func<ProfileInfo, bool> predicate);
    Task<ObjectResult<IEnumerable<ProfileInfo>?>> GetAllAsync();
    Task<bool> Exists(Func<ProfileInfo, bool> predicate);

    // Delete 
    Task<ProfileResult> DeleteAsync(Func<ProfileInfo, bool> predicate);

}

