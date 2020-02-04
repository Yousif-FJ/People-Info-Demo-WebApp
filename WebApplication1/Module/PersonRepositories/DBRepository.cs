using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Module.PersonRepositories
{
    public class DBRepository : IPersonRepository
    {
        private readonly PersonContext context;

        public DBRepository(PersonContext context)
        {
            this.context = context;
        }

        public Person AddPerson(Person person)
        {
            if (person!=null)
            {
                context.People.Add(person);
                context.SaveChanges();
            }
            return person;
        }

        public IEnumerable<Person> GetPeopleBasicInfo()
        {
            return context.People.Select(p => new Person() { ID = p.ID, Name = p.Name });
        }

        public Person GetPerson(int? id)
        {
            id ??= 0;
            return context.People.Find(id);
        }

        public Person DeletePerson(Person person)
        {
            context.People.Remove(person);
            context.SaveChanges();
            return person;
        }

        public Person UpdatePerson(Person person)
        {
            context.Update(person);
            context.SaveChanges();
            return person;
        }
    }
}
