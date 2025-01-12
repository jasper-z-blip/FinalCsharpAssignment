namespace Business.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public required string PostalCode { get; set; }
        public required string City { get; set; }

        public Contact()
        {
            Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return $@"
Id: {Id}, 
FirstName: {FirstName}
LastName: {LastName}
Email: {Email}
Phone: {PhoneNumber}
Address: {Address}, {PostalCode}, {City}";
        }
    }
}
