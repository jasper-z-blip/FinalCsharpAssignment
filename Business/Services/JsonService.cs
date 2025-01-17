using Newtonsoft.Json;

namespace Business.Services
{
    public class JsonService
    {
        private readonly string _filePath;

        public JsonService(string filePath)
        {
            _filePath = filePath;

            EnsureFileExists();
        }

        private void EnsureFileExists()
        {
        // Kontrollera om filen finns, om inte skapa en tom JSON-fil.
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]"); // Initiera som en tom lista.
            }
        }

        public List<T> LoadData<T>()
        {
            try
            {
                string json = File.ReadAllText(_filePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<T>();
                }

                return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
            catch (Exception ex)
            {
                // Logga eller hantera felet, returnerar en tom lista istället för att krascha.
                Console.WriteLine($"Error reading the JSON file: {ex.Message}");
                return new List<T>();
            }
        }

        public void SaveData<T>(List<T> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "The list is empty.");
            }
            try
            {
                // Gör om data till JSON format.
                string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

                // Spara JSON-data i filen.
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                // Logga eller hantera felet.
                Console.WriteLine($"Error writing to JSON file: {ex.Message}");
            }
        }
    }
}
