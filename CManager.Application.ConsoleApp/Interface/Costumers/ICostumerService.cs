using CManager.Domain.ConsoleApp.Models.Costumers;

namespace CManager.Domain.ConsoleApp.Interface.Costumers;

public interface ICostumerService
{
    // Create
    Task<ProfileResult> CreateProfileAsync(ProfileCreateRequest request);

    // Get
    Task<ObjectResult<ProfileInfo>> GetByEmail(string email);
    Task<ObjectResult<IEnumerable<ProfileInfo>?>> GetAllProfilesAsync();

    //Delete
    Task<bool> DeleteByEmail(string email);
}
