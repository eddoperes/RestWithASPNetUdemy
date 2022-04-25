﻿using RestWithASPNetUdemy.Model;

namespace RestWithASPNetUdemy.Services
{
    public interface IPersonService
    {

        Person Create(Person person);

        Person FindById(int id);

        List<Person> FindAll();

        Person Update(Person person);

        void Delete(int id);

    }

}
