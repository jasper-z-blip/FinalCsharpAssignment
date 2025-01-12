namespace Shared.Models
{
    public class Customer
    {
        // Requiered för att man måste fylla i ett värde i alla fält.
        // CustomerNumber för att varje kund ska ha ett unikt nummer, för jag ville att man skulle kunna söka på kunder genom deras customer number.
        public int CustomerNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
        public required string PostalCode { get; set; }
        public required string City { get; set; }

        // Ett unikt ID-nummer för varje kund som skapas.
        public Guid Id { get; set; }

        //Konstruktor som lägger till ett nytt unik GUID för varje customer"
        public Customer()
        {
            Id = Guid.NewGuid();
        }
    }
}