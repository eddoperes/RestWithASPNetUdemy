using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Hypermedia.Utils;
using RestWithASPNetUdemy.Model;

namespace RestWithASPNetUdemy.Business
{
    public interface IBookBusiness
    {

        BookVO Create(BookVO book);

        BookVO FindById(int id);

        List<BookVO> FindAll();

        PagedSearchVO<BookVO> FindWithPagedSearch(string title, string sortDirection, int pageSize, int page);

        BookVO Update(BookVO book);

        void Delete(int id);

    }

}
