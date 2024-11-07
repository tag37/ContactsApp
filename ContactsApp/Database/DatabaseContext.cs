using ContactsApp.Database.DatabaseEntity;
using ContactsApp.Database.DtabaseEntity;

namespace ContactsApp.Database
{
    public class DatabaseContext
    {
        public MetaData MetaData { get; set; } = new MetaData();
        public List<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
