using CManager.Domain.ConsoleApp.Models.Costumers;

namespace CManager.Application.ConsoleApp.Interface.Costumers;

public interface ICostumerService
{
    // Create
    Task<bool> CreateProfileAsync(ProfileCreateRequest createRequest);

    // Get
    Task<ProfileInfo> GetProfileAsync(string Id);
    Task<IReadOnlyList<ProfileInfo>> GetAllProfilesAsync();

    //Delete
    Task<bool> DeleteById(string id);

}
