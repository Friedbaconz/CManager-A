using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using CManager.Domain.ConsoleApp.Models.Costumers;
using CManager.Domain.ConsoleApp.Interface.Costumers;
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
                var json = JsonSerializer.Serialize(profile);
                await File.AppendAllTextAsync(_filePath, json);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeleteProfileById(string id)
        {
            throw new NotImplementedException();
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
