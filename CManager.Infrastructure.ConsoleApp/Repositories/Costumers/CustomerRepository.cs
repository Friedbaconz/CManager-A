using CManager.Domain.ConsoleApp.Interface.Costumers;
using CManager.Domain.ConsoleApp.Models.Costumers;
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

        public async Task<bool> AddProfileRangeAsync(IEnumerable<ProfileInfo> profile)
        {

            try
            {
                if (File.Exists(_filePath))
                {
                    var profileList = new List<ProfileInfo>();
                    var jsonstring = await File.ReadAllTextAsync(_filePath);

                    if (jsonstring != null && jsonstring.Length > 0)
                    {
                        profileList = JsonSerializer.Deserialize<IEnumerable<ProfileInfo>>(jsonstring).ToList();
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
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteProfileById(string id)
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var profileList = new List<ProfileInfo>();
                    var jsonstring = await File.ReadAllTextAsync(_filePath);

                    if (jsonstring != null && jsonstring.Length > 0)
                    {
                        profileList = JsonSerializer.Deserialize<IEnumerable<ProfileInfo>>(jsonstring).ToList();
                    }

                    if (profileList != null)
                    {

                        List<ProfileInfo> transferList = [];
                        foreach (var person in profileList)
                        {
                            if (person.Id == id)
                            {
                                transferList.Remove(person);
                            }
                            else
                            {
                                transferList.Add(person);
                            }
                            
                        }
                        await File.WriteAllTextAsync(_filePath, string.Empty);
                        var json = JsonSerializer.Serialize(transferList);
                        await File.AppendAllTextAsync(_filePath, json);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

        }

        public async Task<IEnumerable<ProfileInfo>> ProfileByAllAsync()
        {
            try
            {
                if (!File.Exists(_filePath)) return [];

                var json = await File.ReadAllTextAsync(_filePath);
                var list = JsonSerializer.Deserialize<IEnumerable<ProfileInfo>>(json);
                return list ?? [];

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return [];
            }
        }

    }
}
