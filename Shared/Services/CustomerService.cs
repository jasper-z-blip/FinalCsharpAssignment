using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Models;

namespace Shared.Services;

public class CustomerService : ICustomerService
{
    // Filhantering används för att läsa och skriva JSON filer.
    private readonly IFileService _fileService;

    // JSON filens namn. Anävnds för att sparas i listan med kunder.
    private const string FileName = "CustomerList.json";

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
            return new ObservableCollection<Customer>();
        }

        string json = await _fileService.ReadFileAsync(FileName);
        Console.WriteLine("Customer list loaded from JSON.");

        // Konverterar JSON till kundlista (Här har jag fått hjälp av chatGPT, så koden är klippt från chatGPT och jag har frågat chatGPT om hjälp vad den gör)
        return JsonConvert.DeserializeObject<ObservableCollection<Customer>>(json) ?? new ObservableCollection<Customer>();
    }

    //Lägger till den skapade kunden i listan och sparar sen listan.
    public async Task AddCustomer(Customer customer)
    {
        Customers.Add(customer);
        await SaveListToJsonFile(Customers);
        Console.WriteLine($"Customer added: {customer.FirstName} {customer.LastName}, Number: {customer.CustomerNumber}");
    }

    // Tar bort den valda kunden från listan och sparar sen listan.
    public async Task RemoveCustomer(Customer customer)
    {
        Customers.Remove(customer);
        await SaveListToJsonFile(Customers);
        Console.WriteLine($"Customer removed: {customer.FirstName} {customer.LastName}");
    }

    // Hämtar nästa kundnummer, lägger på 1 på kundnumret innan, så var kunden innan 1, så blir nästa kund 2 osv..
    public int GetNextCustomerNumber(ObservableCollection<Customer> customers)
    {
        return customers.Any() ? customers.Max(c => c.CustomerNumber) + 1 : 1;
    }
}













