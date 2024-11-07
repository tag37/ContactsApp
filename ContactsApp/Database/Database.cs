using ContactsApp.Database.Interface;
using Newtonsoft.Json;

namespace ContactsApp.Database
{
    public class Database : IDatabase
    {
        public string JsonPath { get; set; }
        public DatabaseContext Context { get; set; }
        public Database(string jsonPath)
        {
            JsonPath = jsonPath;
            if (File.Exists(JsonPath))
            {
                var jsonData = File.ReadAllText(JsonPath);
                Context = JsonConvert.DeserializeObject<DatabaseContext>(jsonData);
            }
            else
            {
                Context = new DatabaseContext();
            }
        }

        public void Save()
        {
            File.WriteAllText(JsonPath, JsonConvert.SerializeObject(Context, Formatting.Indented));
        }
    }
}
