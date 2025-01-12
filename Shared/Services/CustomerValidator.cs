using Shared.Models;
using System.Text.RegularExpressions;

namespace Shared.Services
{
    // Validering för att fälten måste var ifyllda och email har specifik validering med @ och ex dd@dd.se.
    public class CustomerValidator
    {
        public bool Validate(Customer customer, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(customer.FirstName))
            {
                errorMessage = "Firstname is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.LastName))
            {
                errorMessage = "Lastname is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.Email) || !IsEmailValid(customer.Email))
            {
                errorMessage = "A valid Email is required, ex. user@domain.com.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.PhoneNumber))
            {
                errorMessage = "Phonenumber is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.Address))
            {
                errorMessage = "Address is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.PostalCode))
            {
                errorMessage = "Postalcode is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.City))
            {
                errorMessage = "City is required.";
                return false;
            }

            // Om alla fält är ifyllda korrekt så blir errorMessage en tom sträng och valideringen är ok.
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