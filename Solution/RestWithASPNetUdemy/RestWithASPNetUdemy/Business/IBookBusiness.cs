using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;

namespace RestWithASPNetUdemy.Business
{
    public interface IBookBusiness
    {

        BookVO Create(BookVO book);

        BookVO FindById(int id);

        List<BookVO> FindAll();

        BookVO Update(BookVO book);

        void Delete(int id);

    }

}
