using RestWithASPNetUdemy.Data.Converter.Implementations;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Hypermedia.Utils;
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

        public PagedSearchVO<BookVO> FindWithPagedSearch(string title, string sortDirection, int pageSize, int page)
        {
            var sort = !string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = pageSize < 1 ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            var query = @" select * from books where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(title))
                query += $" and title like '%{title}%' ";
            query += $" order by title  {sort} ";
            query += $" offset {offset} rows fetch next {size} rows only ";
            var books = _repository.FindWithPagedSearch(query);

            query = @" select count(*) from books where 1 = 1";
            if (!string.IsNullOrWhiteSpace(title))
                query += $" and title like '%{title}%' ";
            var totalResults = _repository.GetCount(query);

            return new PagedSearchVO<BookVO>
            {
                CurrentPage = page,
                List = _converter.Parse(books),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults,
            };
        }

        public BookVO Update(BookVO book)
        {
            return _converter.Parse(_repository.Update(_converter.Parse(book)));
        }

    }

}
