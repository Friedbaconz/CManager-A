using CManager.Domain.ConsoleApp.Factory;
using CManager.Domain.ConsoleApp.Interface.Costumers;
using CManager.Domain.ConsoleApp.Models.Costumers;
using CManager.Infrastructure.ConsoleApp.Serilization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ProfileInfo = CManager.Domain.ConsoleApp.Models.Costumers.ProfileInfo;

namespace CManager.Infrastructure.ConsoleApp.Repositories.Costumers
{

    public class CustomerRepository(string filepath) : ICostumerRepository
    {
        private readonly string _filePath = filepath;


        public async Task<ProfileResult> Add(ProfileInfo profile)
        {
            var profileList = new List<ProfileInfo>();
            var jsonstring = await File.ReadAllTextAsync(_filePath);

            if (jsonstring != null && jsonstring.Length > 0)
            {
                profileList = JsonSerializer.Deserialize<IEnumerable<ProfileInfo>>(jsonstring)!.ToList();
            }
            if (profileList != null)
            {

                List<ProfileInfo> transferList = [];
                foreach (var person in profileList)
                {
                    transferList.Add(person);
                }

                transferList.AddRange(profile);
                await File.WriteAllTextAsync(_filePath, string.Empty);
                var json = JsonSerializer.Serialize(transferList);
                await File.AppendAllTextAsync(_filePath, json);
                return new ProfileResult (true, "profile added");
            }

            return new ProfileResult(false, "profile couldn't be added");
        }

        public async Task<ProfileResult> AddRangeAsync(IEnumerable<ProfileInfo> profile)
        {
           try
            {
                var profileList = new List<ProfileInfo>();
                var jsonstring = await File.ReadAllTextAsync(_filePath);
                if (jsonstring != null && jsonstring.Length > 0)
                {
                    profileList = JsonSerializer.Deserialize<IEnumerable<ProfileInfo>>(jsonstring)!.ToList();
                }
                if (profileList != null) 
                {
                    List<ProfileInfo> transferList = [];
                    foreach (var person in profile)
                    {
                        transferList.Add(person);
                    }
                    foreach (var person in profileList)
                    {
                        transferList.Add(person);
                    }
                    transferList.AddRange(profile);
                    await File.WriteAllTextAsync(_filePath, string.Empty);
                    var json = JsonSerializer.Serialize(transferList);
                    await File.AppendAllTextAsync(_filePath, json);
                    return new ProfileResult(true, "profile list added");
                }
                return new ProfileResult(false, "profile list couldn't be added");
            }
            catch (Exception ex)
            {
                return new ProfileResult(false, ex.Message);
            }

        }

        public async Task<ProfileResult> DeleteByEmailAsync(string Email)
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var profileList = new List<ProfileInfo>();
                    var jsonstring = await File.ReadAllTextAsync(_filePath);

                    if (jsonstring != null && jsonstring.Length > 0)
                    {
                        profileList = JsonSerializer.Deserialize<IEnumerable<ProfileInfo>>(jsonstring)!.ToList();
                    }

                    if (profileList != null)
                    {

                        List<ProfileInfo> transferList = [];
                        foreach (var person in profileList)
                        {
                            if (person.Id == Email)
                            {
                                transferList.Remove(person);
                            }
                            else
                            {
                                transferList.Add(person);
                            }
                            
                        }
                        await File.WriteAllTextAsync(_filePath, string.Empty);
                        var json = JsonDataFormatter.Serialize(transferList);
                        await File.AppendAllTextAsync(_filePath, json);
                        return new ProfileResult (true, "Profile Deleted" );
                    }
                    return new ProfileResult (false, "Couldn't find Email to delete");
                }
                return new ProfileResult (false, "Couldn't find List " );
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new ProfileResult (false, "Oops something went wrong, tried catching it!");
            }

        }

        public async Task<ObjectResult<IEnumerable<ProfileInfo>?>> GetAllAsync()
        {
            if (!File.Exists(_filePath))
            {

                var json = await File.ReadAllTextAsync(_filePath);
                var list = JsonDataFormatter.Deserialize<IEnumerable<ProfileInfo>?>(json);
                if (list != null)
                    return new ObjectResult<IEnumerable<ProfileInfo>?>(true, "List Found", list);
                return new ObjectResult<IEnumerable<ProfileInfo>?>(true, "Couldn't find a list", []);
            }
            
            return new ObjectResult<IEnumerable<ProfileInfo>?>(true, "Couldn't find a file", []);



        }

        public async Task<bool> Exists(Func<ProfileInfo, bool> predicate)
        {
            if (File.Exists(_filePath))
            {
                var profileList = new List<ProfileInfo>();
                var jsonstring = await File.ReadAllTextAsync(_filePath);
                if (jsonstring != null && jsonstring.Length > 0)
                {
                    profileList = JsonSerializer.Deserialize<IEnumerable<ProfileInfo>>(jsonstring)!.ToList();
                }
                if (profileList != null)
                {
                    foreach (var profile in profileList) 
                    { 
                        if (predicate(profile))
                        {
                            return true;
                        }
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        public async Task<ObjectResult<IEnumerable<ProfileInfo>>?> GetAsync(Func<ProfileInfo, bool> predicate)
        {
            try 
            {
                if (File.Exists(_filePath))
                {
                    var jsonstring = await File.ReadAllTextAsync(_filePath);

                    var profile = JsonDataFormatter.Deserialize<IEnumerable<ProfileInfo>>(jsonstring);

                    if (profile != null)

                        foreach (var person in profile)
                        {
                            if (person.Equals(predicate))
                            {
                                return new ObjectResult<IEnumerable<ProfileInfo>>(true, "", profile);
                            }
                        }
                    return new ObjectResult<IEnumerable<ProfileInfo>>(false, "nothing found", []);

                }

                return new ObjectResult<IEnumerable<ProfileInfo>>(false, "No stored list found", []);
            }
            catch (Exception ex)
            {
                return new ObjectResult<IEnumerable<ProfileInfo>>(false, ex.Message, []);
            }
                
            
        }

    }
}
