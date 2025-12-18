using CManager.Application.ConsoleApp.Helpers.Costumers;
using CManager.Application.ConsoleApp.Validators;
using CManager.Domain.ConsoleApp.Exceptions;
using CManager.Domain.ConsoleApp.Factory;
using CManager.Domain.ConsoleApp.Interface.Costumers;
using CManager.Domain.ConsoleApp.Models.Costumers;
using System.Diagnostics;
using System.Text.Json.Nodes;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ProfileInfo = CManager.Domain.ConsoleApp.Models.Costumers.ProfileInfo;
namespace CManager.Application.ConsoleApp.Services.Costumers;

public sealed class CostumerService : ICostumerService
{

    private readonly ICostumerRepository _costumerRepository;
    private List<ProfileInfo> _profiles = [];

    public CostumerService(ICostumerRepository costumerRepository)
    {
        _costumerRepository = costumerRepository;
        PopulateProfiles();
    }

    public void PopulateProfiles()
    {
        var result = _costumerRepository.GetAllAsync();
        _profiles = result.Result.Result?.ToList() ?? [];
        
    }



    public async Task<ProfileResult> CreateProfileAsync(ProfileCreateRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);


        if (!EmailValidator.IsValidEmail(request.Email))
            return new ProfileResult(false, "Invalid Email");

        if (!NameValidator.IsValidName(request.FirstName))
            return new ProfileResult(false, "Invalid First Name");

        if (!NameValidator.IsValidName(request.LastName))
            return new ProfileResult(false, "Invalid Last Name");

        if (!PhoneValidatorcs.IsValidPhone(request.PhoneNumber))
            return new ProfileResult(false, "Invalid PhoneNumber");


        ProfileInfo profile = ProfileFactory.Create(request);
        _profiles.Add(profile);

       if (await checkProfile(request.Email))
            return new ProfileResult(false, "Profile with this email already exists");

        var profileResult = await _costumerRepository.Add(profile);

        if (!profileResult.Success)
            return new ProfileResult(false, "Profile was unable to be created");

        return profileResult;
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
