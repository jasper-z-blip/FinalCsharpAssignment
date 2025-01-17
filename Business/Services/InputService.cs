
namespace Business.Services
{
    public class InputService
    {
        public static string ReadNonEmptyInput(string fieldName)
        {

            // Loopar tills man skriver in giltig inmatning.
            while (true)
            {
                System.Console.Write($"{fieldName}: ");
                string? input = System.Console.ReadLine(); // Läser inmatningen.

                if (!string.IsNullOrEmpty(input))
                {
                    return input;
                }

                System.Console.WriteLine($"{fieldName} can't be empty");
            }
        }

        public static string ReadValidInput(string fieldName, Func<string, bool> validationFunction, string errorMessage)
        {
            while (true)
            {
                System.Console.Write($"{fieldName}: ");
                string? input = System.Console.ReadLine();

                if (!string.IsNullOrEmpty(input) && validationFunction(input))
                {
                    return input;
                }

                System.Console.WriteLine(errorMessage);
            }
        }
    }
}
