using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Model.Context;

namespace RestWithASPNetUdemy.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {

        private SQLServerContext _context;

        public PersonRepositoryImplementation(SQLServerContext sqlServerContext) 
        {
            _context = sqlServerContext;
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges(); 
            }
            catch (Exception ex)
            {
                throw new Exception("Create Error", ex);
            }
            return person;
        }

        public void Delete(int id)
        {
            var person = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (person != null)
            {
                try
                {
                    _context.Persons.Remove(person);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Delete Error", ex);
                }
            }
        }

        public List<Person> FindAll()
        {
            List<Person> persons = _context.Persons.ToList();
            return persons;
        }

        public Person FindById(int id)
        {
           var person = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
           return person;
        }

        public Person Update(Person person)
        {

            if (!Exists(person.Id))
                return null;

            var person_previous = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

            if (person_previous != null)
            {
                try
                {
                    _context.Entry(person_previous).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Update Error", ex);
                }
            }            
            return person;
        }

        public bool Exists(int id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));  
        }


    }

}
