using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Module.PersonRepositories
{
    public interface IPersonRepository
    {
        Person GetPerson(int? id);
        IEnumerable<Person> GetPeopleBasicInfo();
        Person AddPerson(Person person);
        Person DeletePerson(Person person);
        Person UpdatePerson(Person person);
    }

}
