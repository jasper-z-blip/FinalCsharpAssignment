using Shared.Models;
using System.Text.RegularExpressions;

namespace Shared.Services
{
    public class CustomerValidator
    {
        public bool Validate(Customer customer, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(customer.FirstName))
            {
                errorMessage = "First Name is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.LastName))
            {
                errorMessage = "Last Name is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.Email) || !IsEmailValid(customer.Email))
            {
                errorMessage = "A valid Email is required, ex. user@domain.com.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.PhoneNumber))
            {
                errorMessage = "Phone Number is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.Address))
            {
                errorMessage = "Address is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.PostalCode))
            {
                errorMessage = "Postal Code is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.City))
            {
                errorMessage = "City is required.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        private bool IsEmailValid(string email)
        {
            var emailRegex = @"^(?i)[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$";

            return Regex.IsMatch(email, emailRegex);
        }
    }
}


