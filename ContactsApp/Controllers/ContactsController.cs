using AutoMapper;
using ContactsApp.Database.DtabaseEntity;
using ContactsApp.Database.Interface;
using ContactsApp.ModelDto;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        public IContactRepository ContactRepository { get; }
        public IMapper Mapper { get; }

        public ContactsController(IContactRepository contactRepository, IMapper mapper)
        {
            ContactRepository = contactRepository;
            Mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var contacts = ContactRepository.GetAll();
            return Ok(Mapper.Map<List<ContactResponseDto>>(contacts));
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Invalid contact id.");

            var contact = ContactRepository.GetById(id);

            if (contact == null)
                return NotFound($"Contact id {id} not found.");

            return Ok(Mapper.Map<ContactResponseDto>(contact));

        }

        [HttpPost]
        public IActionResult Post([FromBody] ContactRequestDto requestDto)
        {
            var contact = Mapper.Map<Contact>(requestDto);
            contact = ContactRepository.Insert(contact);
            return Ok(Mapper.Map<ContactResponseDto>(contact));
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Contact contact)
        {
            var dbContact = ContactRepository.GetById(id);

            if (contact == null)
                return NotFound($"Contact id {id} not found.");

            dbContact = ContactRepository.Update(contact, id);
            return Ok(Mapper.Map<ContactResponseDto>(dbContact));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var dbContact = ContactRepository.GetById(id);

            if (dbContact == null)
                return NotFound($"Contact id {id} not found.");
            ContactRepository.Delete(id);

            return NoContent();
        }
    }
}
