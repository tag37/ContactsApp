using AutoMapper;
using ContactsApp.Database.DtabaseEntity;
using ContactsApp.Database.Interface;
using ContactsApp.Infrastructure;
using ContactsApp.ModelDto;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactsController : ApiBaseController
    {
        public IContactRepository ContactRepository { get; }
        public IMapper Mapper { get; }

        public ContactsController(IContactRepository contactRepository, IMapper mapper)
        {
            ContactRepository = contactRepository;
            Mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of all contacts available in the system.
        /// </summary>
        /// <returns>A list of contact details.</returns>
        [HttpGet(Name = "GetAll")]
        [ProducesResponseType(typeof(List<ContactResponseDto>), 200)]
        public IActionResult Get()
        {
            var contacts = ContactRepository.GetAll();
            return Ok(Mapper.Map<List<ContactResponseDto>>(contacts));
        }

        /// <summary>
        /// Retrieves the details of a specific contact by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the contact.</param>
        /// <returns>The contact details for the specified ID.</returns>
        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(typeof(ContactResponseDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public IActionResult Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || !int.TryParse(id, out _))
            {
                return BadRequest("Invalid contact id.");
            }

            var contact = ContactRepository.GetById(id);

            if (contact == null)
                return NotFound($"Contact id {id} not found.");

            return Ok(Mapper.Map<ContactResponseDto>(contact));

        }

        /// <summary>
        /// Creates a new contact record in the system.
        /// </summary>
        /// <param name="requestDto">The contact information to be created.</param>
        /// <returns>The created contact details.</returns>
        [HttpPost(Name = "CreateContact")]
        [ProducesResponseType(typeof(ContactResponseDto), 200)]
        public IActionResult Post([FromBody] ContactRequestDto requestDto)
        {
            if (requestDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contact = Mapper.Map<Contact>(requestDto);
            contact = ContactRepository.Insert(contact);
            return Ok(Mapper.Map<ContactResponseDto>(contact));
        }

        /// <summary>
        /// Updates the information of an existing contact identified by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the contact to be updated.</param>
        /// <param name="requestDto">The updated contact information.</param>
        /// <returns>The updated contact details.</returns>
        [HttpPut("{id}", Name = "UpdateContact")]
        [ProducesResponseType(typeof(ContactResponseDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public IActionResult Put(string id, [FromBody] ContactRequestDto requestDto)
        {
            if (string.IsNullOrWhiteSpace(id) || !int.TryParse(id, out _))
            {
                return BadRequest("Invalid contact id");
            }

            if (requestDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbContact = ContactRepository.GetById(id);

            if (dbContact == null)
                return NotFound($"Contact id {id} not found.");

            var contact = Mapper.Map<Contact>(requestDto);
            dbContact = ContactRepository.Update(contact, id);
            return Ok(Mapper.Map<ContactResponseDto>(dbContact));
        }

        /// <summary>
        /// Deletes the contact record corresponding to the specified unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the contact to be deleted.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}", Name = "DeleteById")]
        [ProducesResponseType(typeof(ErrorResponse), 204)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || !int.TryParse(id, out _))
            {
                return BadRequest("Invalid contact id");
            }

            var dbContact = ContactRepository.GetById(id);

            if (dbContact == null)
                return NotFound($"Contact id {id} not found.");
            ContactRepository.Delete(id);

            return NoContent();
        }
    }
}
