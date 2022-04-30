using RestWithASPNetUdemy.Data.Converter.Implementations;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Hypermedia.Utils;
using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Repository;
using RestWithASPNetUdemy.Repository.Generic;

namespace RestWithASPNetUdemy.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository) 
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

        public PersonVO Disable(int id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);  
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindById(int id)
        {
           return _converter.Parse(_repository.FindById(id));
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            
            var sort = !string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = pageSize < 1 ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            var query = @" select * from person where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name))
                query += $" and first_name like '%{name}%' ";
            query += $" order by first_name  {sort} ";
            query += $" offset {offset} rows fetch next {size} rows only ";
            var persons = _repository.FindWithPagedSearch(query);

            query = @" select count(*) from person where 1 = 1";
            if (!string.IsNullOrWhiteSpace(name))
                query += $" and first_name like '%{name}%' ";
            var totalResults = _repository.GetCount(query);

            return new PagedSearchVO<PersonVO> { 
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,                 
                SortDirections = sort,
                TotalResults = totalResults,
            };
        }

        public PersonVO Update(PersonVO person)
        {
            return _converter.Parse(_repository.Update(_converter.Parse(person)));
        }

    }

}
