using Microsoft.AspNetCore.Mvc;
using Common.Models;
using WebApi_Server.Repositories;

namespace WebApi_Server.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonContext _context;
        private readonly PersonRepository _repository;

        public PersonController(PersonContext context)
        {
            _context = context;
            _repository = new PersonRepository(_context);
        }   

        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            var people = _repository.GetPeople();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public ActionResult<Person> Get(long id)
        {
            var person = _repository.GetPerson(id);

            if (person != null)
            {
                return Ok(person);
            }
            
            return NotFound();
        }

        [HttpPost]
        public ActionResult Post(Person person)
        {
            _repository.AddPerson(person);

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(Person person, long id)
        {
            var successful = _repository.UpdatePerson(person, id);

            if (successful)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            var person = _repository.GetPerson(id);

            if (person != null)
            {
                _repository.DeletePerson(person);
                return Ok(person);
            }

            return NotFound(person);
        }
    }
}
