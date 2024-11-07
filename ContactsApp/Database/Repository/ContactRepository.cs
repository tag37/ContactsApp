using ContactsApp.Database.DtabaseEntity;
using ContactsApp.Database.Interface;

namespace ContactsApp.Database.Implementation
{
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(IDatabase database) : base(database)
        {
        }

        public void Delete(string id)
        {
            var contact = Database.Context.Contacts.FirstOrDefault(i => i.Id == int.Parse(id));
            if (contact != null)
            {
                Database.Context.Contacts.Remove(contact);
                Database.Save();
            }
        }

        public List<Contact> GetAll()
        {
            return Database.Context.Contacts;
        }

        public Contact GetById(string id)
        {
            return Database.Context.Contacts.FirstOrDefault(i => i.Id == int.Parse(id));
        }

        public Contact Insert(Contact entity)
        {
            Database.Context.MetaData.ContactSequenceId = ++Database.Context.MetaData.ContactSequenceId;
            entity.Id = Database.Context.MetaData.ContactSequenceId;
            Database.Context.Contacts.Add(entity);
            Database.Save();
            return entity;
        }

        public Contact Update(Contact entity, string id)
        {
            var index = Database.Context.Contacts.FindIndex(c => c.Id == int.Parse(id));
            if (index != -1)
            {
                entity.Id = Database.Context.Contacts[index].Id;
                Database.Context.Contacts[index] = entity;
                Database.Save();
            }
            return entity;
        }
    }
}
