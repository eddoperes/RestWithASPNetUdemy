using RestWithASPNetUdemy.Data.Converter.Implementations;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Repository.Generic;

namespace RestWithASPNetUdemy.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {

        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository) 
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public BookVO Create(BookVO book)
        {
            return _converter.Parse(_repository.Create(_converter.Parse(book)));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindById(int id)
        {
           return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Update(BookVO book)
        {
            return _converter.Parse(_repository.Update(_converter.Parse(book)));
        }

    }

}
