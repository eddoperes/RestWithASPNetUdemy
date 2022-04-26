using RestWithASPNetUdemy.Model;

namespace RestWithASPNetUdemy.Repository
{
    public interface IBookRepository
    {

        Book Create(Book book);

        Book FindById(int id);

        List<Book> FindAll();

        Book Update(Book book);

        void Delete(int id);

        bool Exists(int id);

    }

}
