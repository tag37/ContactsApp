using Newtonsoft.Json;

namespace ContactsApp.Database
{
    public class DatabaseLoader
    {
        public static DatabaseContext GetDatabaseContext(string jsonFilePath)
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonData = File.ReadAllText(jsonFilePath);
                return JsonConvert.DeserializeObject<DatabaseContext>(jsonData);
            }
            else
            {
                return new DatabaseContext();
            }
        }
    }
}
