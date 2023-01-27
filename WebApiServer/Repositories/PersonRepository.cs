using Common.Models;

namespace WebApi_Server.Repositories
{
    public class PersonRepository
    {
        private readonly PersonContext _context;

        public PersonRepository(PersonContext context)
        {
            _context = context;
        }

        public IList<Person> GetPeople()
        {
            using (var database = _context)
            {
                var people = database.People.ToList();

                return people;
            }
        }

        public Person GetPerson(long id)
        {
            using (var database = _context)
            {
                var person = database.People.Where(p => p.Id == id).FirstOrDefault();

                return person;
            }
        }

        public void AddPerson(Person person)
        {
            using (var database = _context)
            {
                database.People.Add(person);

                database.SaveChanges();
            }
        }

        public bool UpdatePerson(Person person, long id)
        {
            using (var database = _context)
            {
                var dbPerson = database.People.Where(p => p.Id == id).FirstOrDefault();

                if (dbPerson != null)
                {
                    database.People.Update(person);

                    database.SaveChanges();
                    return true; 
                }
         
                return false;
            }
        }

        public void DeletePerson(Person person)
        {
            using (var database = _context)
            {
                database.People.Remove(person);

                database.SaveChanges();
            }
        }
    }
}
