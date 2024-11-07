using ContactsApp.Database.DtabaseEntity;
using ContactsApp.Database.Implementation;
using ContactsApp.Database.Interface;
using NSubstitute;

namespace ContactsApp.UnitTest.Database.Repository
{
    public class ContactRepositoryTest
    {
        private readonly IDatabase _mockDatabase;
        private readonly ContactRepository _repository;

        public ContactRepositoryTest()
        {
            _mockDatabase = Substitute.For<IDatabase>();
            _mockDatabase.Context = new ContactsApp.Database.DatabaseContext();
            _repository = new ContactRepository(_mockDatabase);
        }

        [Fact]
        public void GetAll_ShouldReturnListOfContacts()
        {
            var contacts = new List<Contact>
        {
            new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com" },
            new Contact { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com" }
        };

            _mockDatabase.Context.Contacts = contacts;

            var result = _repository.GetAll();

            Assert.Equal(2, result.Count);
            Assert.Equal(contacts, result);
        }

        [Fact]
        public void GetById_ShouldReturnContact_WhenExists()
        {
            var contact = new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com" };
            _mockDatabase.Context.Contacts = new List<Contact> { contact };
            var result = _repository.GetById("1");
            Assert.Equal(result, contact);
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenNotFound()
        {
            _mockDatabase.Context.Contacts = new List<Contact>();
            var result = _repository.GetById("999");
            Assert.Null(result);
        }

        [Fact]
        public void Insert_ShouldAddContactAndSave()
        {
            var contact = new Contact { FirstName = "John", LastName = "Doe", Email = "john@example.com" };
            _mockDatabase.Context.Contacts = new List<Contact>();


            var contactSequenceId = 100;
            _mockDatabase.Context.MetaData.ContactSequenceId = contactSequenceId;

            var result = _repository.Insert(contact);

            Assert.NotNull(result);
            Assert.Equal(contactSequenceId + 1, result.Id);
            _mockDatabase.Received(1).Save();
        }

        [Fact]
        public void Update_ShouldUpdateContactAndSave()
        {
            var existingContact = new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com" };
            var updatedContact = new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.updated@example.com" };

            _mockDatabase.Context.Contacts = new List<Contact> { existingContact };

            var result = _repository.Update(updatedContact, "1");

            Assert.Equal(updatedContact, result);
            _mockDatabase.Received(1).Save();
        }

        [Fact]
        public void Delete_ShouldRemoveContactAndSave()
        {
            var contact = new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com" };
            _mockDatabase.Context.Contacts = new List<Contact> { contact };
            _repository.Delete("1");

            _mockDatabase.Received(1).Save();
            Assert.Empty(_mockDatabase.Context.Contacts);
        }
    }
}
