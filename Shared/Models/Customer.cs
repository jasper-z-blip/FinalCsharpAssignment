namespace Shared.Models
{
    public class Customer
    {
        public int CustomerNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
        public required string PostalCode { get; set; }
        public required string City { get; set; }
        public Guid Id { get; set; }

        public Customer()
        {
            Id = Guid.NewGuid();
        }
    }
}







