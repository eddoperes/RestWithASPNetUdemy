using RestWithASPNetUdemy.Data.Converter.Contract;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;

namespace RestWithASPNetUdemy.Data.Converter.Implementations
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {

        public Person Parse(PersonVO origin)
        {
            if (origin == null) return null;
            return new Person { 
                Id = origin.Id,
                Address = origin.Address,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Gender = origin.Gender,                                        
            };
        }

        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(Item => Parse(Item)).ToList();  
        }

        public PersonVO Parse(Person origin)
        {
            if (origin == null) return null;
            return new PersonVO
            {
                Id = origin.Id,
                Address = origin.Address,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Gender = origin.Gender,
            };
        }

        public List<PersonVO> Parse(List<Person> origin)
        {
            if (origin == null) return null;
            return origin.Select(Item => Parse(Item)).ToList();
        }

    }
}
