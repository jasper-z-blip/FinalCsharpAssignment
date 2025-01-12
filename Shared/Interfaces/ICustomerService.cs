using System.Collections.ObjectModel;
using Shared.Models;

namespace Shared.Interfaces
{
    public interface ICustomerService
    {
        Task SaveListToJsonFile(ObservableCollection<Customer> customers);
        Task<ObservableCollection<Customer>> LoadListFromJsonFile();
        Task AddCustomer(Customer customer);
        int GetNextCustomerNumber(ObservableCollection<Customer> customers);
    }
}




