using CManager.Domain.ConsoleApp.Interface.Costumers;
using CManager.Domain.ConsoleApp.Models.Costumers;
using CManager.Application.ConsoleApp.Helpers.Costumers;
using System.Diagnostics;
using CManager.Infrastructure.ConsoleApp.Repositories.Costumers;
using ProfileInfo = CManager.Domain.ConsoleApp.Models.Costumers.ProfileInfo;
namespace CManager.Application.ConsoleApp.Services.Costumers;

public class CostumerService(ICostumerRepository costumerRepository) : ICostumerService
{
    
    

    private readonly ICostumerRepository _costumerRepository = costumerRepository;
    private List <ProfileInfo> _profileList = [];

    public async Task<bool> CreateProfileAsync(string firstName, string lastname, string email, string phonenumber, AddressInfo address)
    {
        try
        {
            var id = IdGenerator.Generate();

            var profile = new ProfileInfo
            {
                FirstName = firstName,
                LastName = lastname,
                Email = email,
                PhoneNumber = phonenumber,
                Id = id,
                AddressProfile = address
            };

            _profileList.Add(profile);
            var result = await _costumerRepository.AddProfileRangeAsync(_profileList);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteByEmail(string email)
    {
        try
        {
            var profile = _profileList.FirstOrDefault(x => x.Email == email);
            if (profile != null)
            {
               
                _profileList.Remove(profile);
                await _costumerRepository.DeleteProfileById(email);

            }
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<IReadOnlyList<ProfileInfo>> GetAllProfilesAsync()
    {
        _profileList = [.. await _costumerRepository.ProfileByAllAsync()];
        return _profileList;
    }

    public async Task<ProfileInfo> GetProfileAsync(string Id)
    {
        _profileList = [.. await _costumerRepository.ProfileByAllAsync()];

        var profile = _profileList.FirstOrDefault(x => x.Id == Id);
        return profile!;
    }
}
