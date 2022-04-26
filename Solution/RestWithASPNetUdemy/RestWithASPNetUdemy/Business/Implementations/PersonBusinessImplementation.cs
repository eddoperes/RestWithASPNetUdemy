using RestWithASPNetUdemy.Data.Converter.Implementations;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Repository.Generic;

namespace RestWithASPNetUdemy.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IRepository<Person> repository) 
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
            return _converter.Parse(_repository.Create(_converter.Parse(person)));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindById(int id)
        {
           return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Update(PersonVO person)
        {
            return _converter.Parse(_repository.Update(_converter.Parse(person)));
        }

    }

}
