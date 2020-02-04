using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Module.PersonRepositories
{

    public class MemoryPersonRepository : IPersonRepository
    {
        public MemoryPersonRepository()
        {
            _People = new List<Person>()
            {
                new Person(){ ID= 1 , Name = "kento", Age= 28, Sector=Sector.Div},
                new Person(){ ID= 2,  Name="jerry" , Age= 30, Sector=Sector.HR}
            };
        }

        private readonly List<Person> _People;

        public IEnumerable<Person> GetPeopleBasicInfo()
        {
            return _People;
        }

        public Person GetPerson(int? id)
        {
            id ??= 0;
            return _People.FirstOrDefault(p => p.ID == id);
        }

        public Person AddPerson(Person person)
        {
            if (person is null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            person.ID = _People.Max(p => p.ID) + 1;
            _People.Add(person);
            return person;
        }

        public Person DeletePerson(Person person)
        {
            _People.Remove(person);
            return person;           
        }

        public Person UpdatePerson(Person person)
        {
            if (person is null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            _People[--person.ID] = person;
            return person;
        }
    }
}
