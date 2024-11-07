namespace ContactsApp.Database.Interface
{
    public interface IDatabase
    {
        public DatabaseContext Context { get; set; }
        public void Save();
    }
}
