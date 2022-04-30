using Microsoft.EntityFrameworkCore;
using RestWithASPNetUdemy.Model.Base;
using RestWithASPNetUdemy.Model.Context;

namespace RestWithASPNetUdemy.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected SQLServerContext _context;
        private DbSet<T> _dataSet;

        public GenericRepository(SQLServerContext sqlServerContext)
        {
            _context = sqlServerContext;
            _dataSet = _context.Set<T>(); 
        }

        public T Create(T item)
        {
            try
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Create Error", ex);
            }
            return item;
        }

        public void Delete(int id)
        {
            var item = _dataSet.SingleOrDefault(p => p.Id.Equals(id));
            if (item != null)
            {
                try
                {
                    _dataSet.Remove(item);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Delete Error", ex);
                }
            }
        }

        public List<T> FindAll()
        {
            List<T> persons = _dataSet.ToList();
            return persons;
        }

        public T FindById(int id)
        {
            var item = _dataSet.SingleOrDefault(i => i.Id.Equals(id));
            return item;
        }

        public T Update(T item)
        {

            if (!Exists(item.Id))
                return null;

            var item_previous = _dataSet.SingleOrDefault(i => i.Id.Equals(item.Id));

            if (item_previous != null)
            {
                try
                {
                    _context.Entry(item_previous).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return item;
                }
                catch (Exception ex)
                {
                    throw new Exception("Update Error", ex);
                }
            }
            else 
            {
                return null;
            }
            
        }

        public bool Exists(int id)
        {
            return _dataSet.Any(i => i.Id.Equals(id));
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return _dataSet.FromSqlRaw<T>(query).ToList();  
        }

        public int GetCount(string query)
        {
            string count = "";
            using (var connection = _context.Database.GetDbConnection()) 
            {
                connection.Open();
                using (var command = connection.CreateCommand()) 
                {
                    command.CommandText = query;
                    count  = command.ExecuteScalar().ToString();
                }                            
            }
            return int.Parse(count);
        }

    }
}
