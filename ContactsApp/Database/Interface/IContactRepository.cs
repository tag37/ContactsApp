using ContactsApp.Database.DtabaseEntity;

namespace ContactsApp.Database.Interface
{
    public interface IContactRepository
    {
        public Contact Insert(Contact entity);
        public Contact Update(Contact entity, string id);
        public void Delete(string id);
        public List<Contact> GetAll();
        public Contact GetById(string id);
    }
}
