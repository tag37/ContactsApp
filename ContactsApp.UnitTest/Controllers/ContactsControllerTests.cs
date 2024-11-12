using AutoMapper;
using ContactsApp.Controllers;
using ContactsApp.Database.DtabaseEntity;
using ContactsApp.Database.Interface;
using ContactsApp.Infrastructure;
using ContactsApp.ModelDto;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace ContactsApp.UnitTest.Controllers
{

    public class ContactsControllerTests
    {
        private readonly IContactRepository _mockContactRepository;
        private readonly IMapper _mockMapper;
        private readonly ContactsController _controller;

        public ContactsControllerTests()
        {
            _mockContactRepository = Substitute.For<IContactRepository>();
            _mockMapper = Substitute.For<IMapper>();
            _controller = new ContactsController(_mockContactRepository, _mockMapper);
        }

        [Fact]
        public void Get_ShouldReturnOkResult_WithContactsList()
        {
            var contacts = new List<Contact>
        {
            new Contact { Id = 1, FirstName = "Tushar", LastName = "Ghulaxe", Email = "tushar@example.com" },
            new Contact { Id = 2, FirstName = "MAyur", LastName = "Ghulaxe", Email = "mayur@example.com" }
        };

            var contactDtos = new List<ContactResponseDto>
        {
            new ContactResponseDto { Id = 1, FirstName = "Tushar", LastName = "Ghulaxe", Email = "tushar@example.com" },
            new ContactResponseDto { Id = 2, FirstName = "MAyur", LastName = "Ghulaxe", Email = "mayur@example.com" }
        };

            _mockContactRepository.GetAll().Returns(contacts);
            _mockMapper.Map<List<ContactResponseDto>>(contacts).Returns(contactDtos);

            var result = _controller.Get();

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, contactDtos);
        }

        [Fact]
        public void Get_ById_ShouldReturnOkResult_WithContact()
        {
            var contact = new Contact { Id = 1, FirstName = "Tushar", LastName = "Ghulaxe", Email = "tushar@example.com" };
            var contactDto = new ContactResponseDto { Id = 1, FirstName = "Tushar", LastName = "Ghulaxe", Email = "tushar@example.com" };

            _mockContactRepository.GetById("1").Returns(contact);
            _mockMapper.Map<ContactResponseDto>(contact).Returns(contactDto);

            var result = _controller.Get("1");

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, contactDto);
        }

        [Fact]
        public void Get_ById_ShouldReturnBadRequest_WhenIdIsInvalid()
        {
            var result = _controller.Get("");

            var badRequestResult = result as ObjectResult;
            var response = badRequestResult?.Value as ErrorResponse;
            Assert.NotNull(response);
            Assert.Equal("400", response.Status);
            Assert.Equal("Invalid contact id.", response.Details);
        }

        [Fact]
        public void Post_ShouldReturnOkResult_WithCreatedContact()
        {
            var contactRequestDto = new ContactRequestDto { FirstName = "Tushar", LastName = "Ghulaxe", Email = "Tushar@example.com" };
            var contact = new Contact { Id = 1, FirstName = "Tushar", LastName = "Ghulaxe", Email = "Tushar@example.com" };
            var contactDto = new ContactResponseDto { Id = 1, FirstName = "Tushar", LastName = "Ghulaxe", Email = "Tushar@example.com" };

            _mockMapper.Map<Contact>(contactRequestDto).Returns(contact);
            _mockContactRepository.Insert(contact).Returns(contact);
            _mockMapper.Map<ContactResponseDto>(contact).Returns(contactDto);

            var result = _controller.Post(contactRequestDto);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, contactDto);
        }

        [Fact]
        public void Put_ShouldReturnOkResult_WithUpdatedContact()
        {
            var contactId = "1";
            var existingContact = new Contact { Id = 1, FirstName = "Tushar", LastName = "Ghulaxe", Email = "Tushar@example.com" };
            var updatedContact = new Contact { Id = 1, FirstName = "Tushar", LastName = "Ghulaxe", Email = "Tushar.updated@example.com" };
            var updatedContactReq = new ContactRequestDto { FirstName = "Tushar", LastName = "Ghulaxe", Email = "Tushar.updated@example.com" };
            var updatedContactDto = new ContactResponseDto { Id = 1, FirstName = "Tushar", LastName = "Ghulaxe", Email = "Tushar.updated@example.com" };

            _mockContactRepository.GetById(contactId).Returns(updatedContact);
            _mockContactRepository.Update(Arg.Any<Contact>(), contactId).Returns(updatedContact);
            _mockMapper.Map<ContactResponseDto>(updatedContact).Returns(updatedContactDto);
            _mockMapper.Map<Contact>(Arg.Any<ContactRequestDto>()).Returns(existingContact);

            var result = _controller.Put(contactId, updatedContactReq);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, updatedContactDto);
        }

        [Fact]
        public void Delete_ShouldReturnNoContent_WhenContactIsDeleted()
        {
            // Arrange
            var contactId = "1";
            var contact = new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com" };

            _mockContactRepository.GetById(contactId).Returns(contact);
            _mockContactRepository.Delete(contactId);

            // Act
            var result = _controller.Delete(contactId);

            // Assert
            var noContentResult = result as NoContentResult;

            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
        }
    }
}
