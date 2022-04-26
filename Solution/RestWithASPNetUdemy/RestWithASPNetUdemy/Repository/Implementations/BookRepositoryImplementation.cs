using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Model.Context;

namespace RestWithASPNetUdemy.Repository.Implementations
{
    public class BookRepositoryImplementation : IBookRepository
    {

        private SQLServerContext _context;

        public BookRepositoryImplementation(SQLServerContext sqlServerContext) 
        {
            _context = sqlServerContext;
        }

        public Book Create(Book book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges(); 
            }
            catch (Exception ex)
            {
                throw new Exception("Create Error", ex);
            }
            return book;
        }

        public void Delete(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id.Equals(id));
            if (book != null)
            {
                try
                {
                    _context.Books.Remove(book);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Delete Error", ex);
                }
            }
        }

        public List<Book> FindAll()
        {
            List<Book> books = _context.Books.ToList();
            return books;
        }

        public Book FindById(int id)
        {
           var book = _context.Books.SingleOrDefault(b => b.Id.Equals(id));
           return book;
        }

        public Book Update(Book book)
        {

            if (!Exists(book.Id))
                return null;

            var book_previous = _context.Persons.SingleOrDefault(p => p.Id.Equals(book.Id));

            if (book_previous != null)
            {
                try
                {
                    _context.Entry(book_previous).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Update Error", ex);
                }
            }            
            return book;
        }

        public bool Exists(int id)
        {
            return _context.Books.Any(b => b.Id.Equals(id));  
        }


    }

}
