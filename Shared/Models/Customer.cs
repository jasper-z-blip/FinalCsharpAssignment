namespace Shared.Models
{
    public class Customer
    {
        public int CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public Guid Id { get; set; }

        public Customer()
        {
            Id = Guid.NewGuid();
        }
    }
}







