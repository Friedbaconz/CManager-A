using CManager.Application.ConsoleApp.Interface.Costumers;
using CManager.Domain.ConsoleApp.Models.Costumers;
using CManager.Application.ConsoleApp.Helpers.Costumers;
using System.Diagnostics;
using ProfileInfo = CManager.Domain.ConsoleApp.Models.Costumers.ProfileInfo;
using AdressInfo = CManager.Domain.ConsoleApp.Models.Costumers.AdressInfo;

namespace CManager.Application.ConsoleApp.Services.Costumers;

public class CostumerService(ICostumerRepository costumerRepository) : ICostumerService
{
    private readonly ICostumerRepository _costumerRepository = costumerRepository;
    private readonly AdressInfo _addressInfo = new AdressInfo();
    private List <ProfileInfo> _profileList = [];

    public async Task<bool> CreateProfileAsync(ProfileCreateRequest createRequest)
    {
        try
        {

            
            var Ort = _addressInfo.ort;
            var PostNumbers = _addressInfo.postNumbers;
            var StreetName = _addressInfo.streetName;
            var Address = new AdressInfo
            {
                streetName = StreetName,
                postNumbers = PostNumbers,
                ort = Ort
            };

            var adressInfo = Address;
            var FirstName = createRequest.FirstName;
            var LastName = createRequest.LastName;
            var PhoneNumber = createRequest.PhoneNumber;
            var Email = createRequest.Email;

            var profile = new ProfileInfo
            {
                FirstName = FirstName,
                LastName = LastName,
                Id = IdGenerator.Generate(),
                PhoneNumber = PhoneNumber,
                Email = Email,
                AdressProfile = adressInfo
            };
            _profileList.Add(profile);
            var result = await _costumerRepository.AddProfileRange(_profileList);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteById(string id)
    {
        try
        {
            var profile = _profileList.FirstOrDefault(x => x.Id == id);
            if (profile != null)
            {
               
                _profileList.Remove(profile);
                return _costumerRepository.DeleteProfileById(id);

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
        _profileList = [.. await _costumerRepository.ProfileByAll()];
        return _profileList;
    }

    public async Task<ProfileInfo> GetProfileAsync(string Id)
    {
        _profileList = [.. await _costumerRepository.ProfileByAll()];

        var profile = _profileList.FirstOrDefault(x => x.Id == Id);
        return profile!;
    }
}
