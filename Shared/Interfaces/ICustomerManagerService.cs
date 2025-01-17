using Shared.Models;
using System.Collections.ObjectModel;

namespace Shared.Interfaces
{
    public interface ICustomerManagerService
    {
        Task<ObservableCollection<Customer>> LoadCustomersAsync();
        Task AddCustomerAsync(Customer newCustomer);
        Task<bool> DeleteCustomerAsync(Customer customerToDelete);
        Task<bool> UpdateCustomerAsync(Customer updatedCustomer);
        Task<int> GetNextCustomerNumberAsync();
    }
}