using Newtonsoft.Json;

namespace Business.Services // Hela koden tagen från chatGPT
{
    public class JsonService
    {
        private readonly string _filePath;

        public JsonService(string filePath)
        {
            _filePath = filePath;

            // Kontrollera om filen finns, om inte skapa en tom JSON-fil
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]"); // Initiera som en tom lista
            }
        }

        /// <summary>
        /// Läser och deserialiserar JSON-data från filen till en lista av typ T.
        /// </summary>
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
                // Logga eller hantera felet
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return new List<T>();
            }
        }

        /// <summary>
        /// Serialiserar och sparar en lista av typ T till JSON-filen.
        /// </summary>
        public void SaveData<T>(List<T> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            try
            {
                string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                // Logga eller hantera felet
                Console.WriteLine($"Error writing to JSON file: {ex.Message}");
            }
        }
    }
}
