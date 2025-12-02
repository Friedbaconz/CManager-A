using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using CManager.Application.ConsoleApp.Interface.Costumers;
using CManager.Domain.ConsoleApp.Models.Costumers;

namespace CManager.Infrastructure.ConsoleApp.Repositories.Costumers
{
    
    public class CustomerRepository(string filepath) : ICostumerRepository
    {
        private readonly string _filePath = filepath;

        public async Task<bool> AddProfileRange(IEnumerable<ProfileInfo> profile)
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

        public async Task<IEnumerable<ProfileInfo>> ProfileByAll()
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
