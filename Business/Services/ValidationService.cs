using System.Text.RegularExpressions;

namespace Business.Services
{
    public static class ValidationService
    {
        // Metod som kollar om namn är ifyllda, så dom inte är null eller whitespace. Samt att dom följer Regex.
        public static bool IsValidName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            string nameRegex = @"^(?=.*[a-zA-ZäöåÄÖÅ].*[a-zA-ZäöåÄÖÅ])([a-zA-ZäöåÄÖÅ]+([ -][a-zA-ZäöåÄÖÅ]+)*)$"; // kopierat från chatGPT (kollar så namnet innehåller minst två bokstäver ,a-ö, stora eller små spelar ingen roll, får innehålla mellanslag och -)

            return Regex.IsMatch(name.Trim(), nameRegex);
        }

        // Metod som kollar om email är ifyllt, så den inte är null eller whitespace. Samt att den följer Regex.
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // kopierat från chatGPT (kollar så det är en giltig epostadress, med tecken framför @ och tecken bakom @, att det är med @, att det avslutas med . och sen tex. se, com osv..)
            return Regex.IsMatch(email, emailRegex);
        }

        public static bool IsValidPhone(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            string phoneRegex = @"^\d{10}$"; // kopierat från chatGPT (kollar så att det är ett gitligt mobilnummer med 10 siffror och inga mellanslag eller specialtecken.)
            return Regex.IsMatch((string)phoneNumber, phoneRegex);
        }

        public static bool IsValidPostalCode(string postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
                return false;

            string postalRegex = @"^\d{5}$"; // kopierat från chatGPT (kollar så postnumret innehåller 5 siffor och inga andra tecken än siffror)
            return Regex.IsMatch(postalCode, postalRegex);
        }
    }
}

