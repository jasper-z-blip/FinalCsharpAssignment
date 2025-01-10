using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Models;
using System.Collections.ObjectModel;
using Microsoft.Maui.Storage;

namespace Shared.Services
{
    public class CustomerService : ICustomerService
    {
        private const string FileName = "CustomerList.json";
        private string FilePath => Path.Combine(FileSystem.AppDataDirectory, FileName);

        public async Task SaveListToJsonFile(ObservableCollection<Customer> customers)
        {
            string json = JsonConvert.SerializeObject(customers, Formatting.Indented);
            await File.WriteAllTextAsync(FilePath, json);
        }

        public async Task<ObservableCollection<Customer>> LoadListFromJsonFile()
        {
            if (!File.Exists(FilePath))
                return new ObservableCollection<Customer>();

            string json = await File.ReadAllTextAsync(FilePath);
            return JsonConvert.DeserializeObject<ObservableCollection<Customer>>(json) ?? new ObservableCollection<Customer>();
        }

        public int GetNextCustomerNumber(ObservableCollection<Customer> customers)
        {
            // Kontrollera om det finns några kunder, börja på 1 annars
            return customers.Any() ? customers.Max(c => c.CustomerNumber) + 1 : 1;
        }

    }
}









