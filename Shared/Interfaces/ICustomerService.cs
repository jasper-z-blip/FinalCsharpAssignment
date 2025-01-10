using System.Collections.ObjectModel;
using Shared.Models;

namespace Shared.Interfaces
{
    public interface ICustomerService
    {
        Task SaveListToJsonFile(ObservableCollection<Customer> customers);
        Task<ObservableCollection<Customer>> LoadListFromJsonFile();
        int GetNextCustomerNumber(ObservableCollection<Customer> customers);
    }
}




