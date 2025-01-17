using Shared.Interfaces;
using Shared.Models;
using System.Collections.ObjectModel;

//CustomerManagerService bygger på CustomerService och lägger till affärslogik, regler för hur det ska hanteras.
namespace Shared.Services
{
    // Detta ska ladda, lägga till, radera och uppdatera kunder.
    public class CustomerManagerService : ICustomerManagerService
    {
        private readonly ICustomerService _customerService;

        public CustomerManagerService(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // Laddar alla kunder från JSON fil och retunera som ObservableCollection.
        public async Task<ObservableCollection<Customer>> LoadCustomersAsync()
        {
            return await _customerService.LoadListFromJsonFile();
        }

        public async Task AddCustomerAsync(Customer newCustomer)
        {
            var validator = new CustomerValidator();
            if (!validator.Validate(newCustomer, out string errorMessage))
            {
                throw new InvalidOperationException($"Validation failed: {errorMessage}");
            }

            var customers = await _customerService.LoadListFromJsonFile();
            customers.Add(newCustomer);
            await _customerService.SaveListToJsonFile(customers);
        }

        // Tar bort om den finns i sparade listan.
        public async Task<bool> DeleteCustomerAsync(Customer customerToDelete)
        {
            // Ladda kunder från JSON.
            var customers = await _customerService.LoadListFromJsonFile();

            // Försök att ta bort kunden.
            if (customers.Remove(customerToDelete))
            {
                // Om borttagning lyckades, spara listan tillbaka till filen.
                await _customerService.SaveListToJsonFile(customers);
                return true; // Kunden togs bort framgångsrikt.
            }

            // Om kunden inte fanns i listan.
            return false;
        }

        // Updaterar kundinfon om det finns en.
        public async Task<bool> UpdateCustomerAsync(Customer updatedCustomer)
        {
            var validator = new CustomerValidator();
            if (!validator.Validate(updatedCustomer, out string errorMessage))
            {
                throw new InvalidOperationException($"Validation failed: {errorMessage}");
            }

            var customers = await _customerService.LoadListFromJsonFile();
            
            if (customers.Any(c => c.CustomerNumber == updatedCustomer.CustomerNumber && c.Id != updatedCustomer.Id))
            {
                throw new InvalidOperationException($"A customer with the number {updatedCustomer.CustomerNumber} already exists.");
            }

            var existingCustomer = customers.FirstOrDefault(c => c.Id == updatedCustomer.Id);
            if (existingCustomer != null)
            {
                // Här uppdateras kunddatan.
                existingCustomer.FirstName = updatedCustomer.FirstName;
                existingCustomer.LastName = updatedCustomer.LastName;
                existingCustomer.Email = updatedCustomer.Email;
                existingCustomer.PhoneNumber = updatedCustomer.PhoneNumber;
                existingCustomer.Address = updatedCustomer.Address;
                existingCustomer.PostalCode = updatedCustomer.PostalCode;
                existingCustomer.City = updatedCustomer.City;
                existingCustomer.CustomerNumber = updatedCustomer.CustomerNumber;

                await _customerService.SaveListToJsonFile(customers);
                return true;
            }
            return false;
        }

        
        public async Task<int> GetNextCustomerNumberAsync()
        {
            var customers = await _customerService.LoadListFromJsonFile();

            // Samla alla existerande kundnummer
            var customerNumbers = customers.Select(c => c.CustomerNumber).OrderBy(n => n).ToList();

            int nextNumber = 1;

            /*När vi går igenom listan med existerande kundnummer i stigande ordning,
            förväntar vi oss att varje nummer ska matcha det nuvarande värdet av `nextNumber`.
            Om det inte gör det betyder det att det saknas ett nummer i följden (ett "hål").
            Eftersom listan är sorterad, vet vi att det första numret som inte matchar `nextNumber`
            är det minsta lediga kundnumret. Därför kan vi avsluta loopen och använda det
            aktuella värdet av `nextNumber` som det nästa lediga numret. (Tog hjälp av chatGPT att förklara för jag kom inte på hur jag skulle förklara vad som händer här på ett bra sätt)*/

            foreach ( var customerNumber in customerNumbers )
            {
                if (customerNumber == nextNumber)
                {
                    nextNumber++;
                }
                else
                {
                    break;
                }
            }
            return nextNumber;
        }
    }
}