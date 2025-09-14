using Entity;
using ServiceContract.DTO.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract
{
    public interface IPerson
    {
        public PersonRes AddPerson(PersonAddReq req);
        public PersonRes EditPerson(PersonEditReq req);
        public bool DeletePerson(Guid guid);
        public List<PersonRes> GetAllPerson();
        public PersonRes GetPersonById(Guid? guid);
    }
}
