using CManager.Application.ConsoleApp.Helpers.Costumers;
using CManager.Domain.ConsoleApp.Exceptions;
using CManager.Domain.ConsoleApp.Factory;
using CManager.Domain.ConsoleApp.Interface.Costumers;
using CManager.Domain.ConsoleApp.Models.Costumers;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ProfileInfo = CManager.Domain.ConsoleApp.Models.Costumers.ProfileInfo;
namespace CManager.Application.ConsoleApp.Services.Costumers;

public class CostumerService(ICostumerRepository costumerRepository) : ICostumerService
{
    
    private readonly ICostumerRepository _costumerRepository = costumerRepository;


    public async Task<ProfileResult> CreateProfileAsync(ProfileCreateRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        ProfileInfo profile = ProfileFactory.Create(request);
        _profiles.Add(profile);

        if (await checkProfile(profile.Email))
            throw new DomainException("Profile Already Exists!");
        var profileResult = await _costumerRepository.Add(profile);
        if (!profileResult.Success)
            return new ProfileResult(false, "Profile was unable to be created");

        return new ProfileResult(false, "Profile was created");
    }

    public async Task<bool> DeleteByEmail(string email)
    {
        var trashprofile = await _costumerRepository.DeleteByEmailAsync(email);
        if (!trashprofile.Success)
            return true;

        return false;  
    }

    public async Task<ObjectResult<IEnumerable<ProfileInfo>?>> GetAllProfilesAsync()
    {
        var profilelist = await _costumerRepository.GetAllAsync();
        if (!profilelist.Success)
            return new ObjectResult<IEnumerable<ProfileInfo>?>(true, "No Members Found", []);

        return new ObjectResult<IEnumerable<ProfileInfo>?> (true, "List gotten", profilelist.Result);
    }

    public async Task<ObjectResult<ProfileInfo>> GetByEmail(string email)
    {
        var profilelist = await _costumerRepository.GetAsync(x => x.Email == email);

        
        if (!profilelist.Success)
            return new ObjectResult<ProfileInfo> (false, "No Member Found.", new ProfileInfo());
        if (profilelist.Result != null)
        {
            foreach (var profile in profilelist.Result)
            {
                if (profile.Email == email)
                {
                    return new ObjectResult<ProfileInfo>(true, "Profile Found", profile);
                }
            }
        }
        return new ObjectResult<ProfileInfo>(true, "No Member Found.", new ProfileInfo());
    }

    public async Task <bool> checkProfile(string email) => await _costumerRepository.Exists(x => x.Email == email);
    
}
