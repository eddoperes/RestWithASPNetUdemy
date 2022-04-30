using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Repository.Generic;

namespace RestWithASPNetUdemy.Repository
{
    public interface IPersonRepository: IRepository<Person>
    {

        Person Disable(int id);

        List<Person> FindByName(string firstName, string lastName);

    }

}
