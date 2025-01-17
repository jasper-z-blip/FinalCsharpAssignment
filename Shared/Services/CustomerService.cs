using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Models;

namespace Shared.Services;

// CustomerService hanterar filoperationer, vet inget om reglerna, hanterar att spara och läsa data.
// Tanken är att flytta AddCustomer och RemoveCustomer till CustomerManagerService som jag gjort med GetNextCustomer när jag skapade CustomerManagerService, men jag väljer att inte göra det nu, då det är en del att bygga om.
public class CustomerService : ICustomerService
{
    // Filhantering används för att läsa och skriva JSON filer.
    private readonly IFileService _fileService;

    // JSON filens namn. Anävnds för att sparas i listan med kunder.
    private const string FileName = "CustomerList.json";

    // En lista som håller kunddata i minnet.
    public ObservableCollection<Customer> Customers { get; private set; } = new ObservableCollection<Customer>();

    public CustomerService(IFileService fileService)
    {
        _fileService = fileService;
        _ = InitializeCustomers();
    }

    private async Task InitializeCustomers()
    {
        Customers = await LoadListFromJsonFile();
        Console.WriteLine($"Initialized with {Customers.Count} customers.");
    }

    public async Task SaveListToJsonFile(ObservableCollection<Customer> customers)
    {
        string json = JsonConvert.SerializeObject(customers, Formatting.Indented);
        await _fileService.WriteFileAsync(FileName, json);
        Console.WriteLine("Customer list saved to JSON.");
    }

    public async Task<ObservableCollection<Customer>> LoadListFromJsonFile()
    {
        if (!_fileService.FileExists(FileName))
        {
            Console.WriteLine("Customer file not found. Returning empty list.");
            Customers = new ObservableCollection<Customer>();
            return Customers;
        }

        string json = await _fileService.ReadFileAsync(FileName);
        Console.WriteLine("Customer list loaded from JSON.");

        // Uppdatera `Customers` med innehållet från filen.
        Customers = JsonConvert.DeserializeObject<ObservableCollection<Customer>>(json) ?? new ObservableCollection<Customer>();
        return Customers;
    }

    // Lägger till den skapade kunden i listan och sparar sen listan.
    // Kontroll att kundnumret är unikt.
    public async Task AddCustomer(Customer customer)
    {
        var customers = await LoadListFromJsonFile();

        // Kontrollera om kundnumret redan används.
        if (customers.Any(c => c.CustomerNumber == customer.CustomerNumber))
        {
            throw new InvalidOperationException($"A customer with the number {customer.CustomerNumber} already exists.");
        }

        customers.Add(customer);
        await SaveListToJsonFile(customers);

        // Uppdatera den interna listan med nya data från filen.
        Customers = new ObservableCollection<Customer>(customers);
    }

    // Tar bort den valda kunden från listan och sparar sen listan.
    public async Task RemoveCustomer(Customer customer)
    {
        // Ladda den senaste listan från fil.
        var customers = await LoadListFromJsonFile();

        // Hitta och ta bort kunden.
        var customerToRemove = customers.FirstOrDefault(c => c.Id == customer.Id);
        if (customerToRemove != null)
        {
            customers.Remove(customerToRemove);
            await SaveListToJsonFile(customers);

            Customers = new ObservableCollection<Customer>(customers);

            Console.WriteLine($"Customer removed: {customer.FirstName} {customer.LastName}");
        }
        else
        {
            Console.WriteLine("Customer not found for removal.");
        }
    }
}