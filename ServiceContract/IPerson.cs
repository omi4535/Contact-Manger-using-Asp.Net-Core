using Entity;
using ServiceContract.DTO.Person;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceContract
{
    public interface IPerson
    {
        Task<PersonRes> AddPersonAsync(PersonAddReq req);
        Task<PersonRes> EditPersonAsync(PersonEditReq req);
        Task<bool> DeletePersonAsync(Guid guid);
        Task<List<PersonRes>> GetAllPersonAsync();
        Task<PersonRes?> GetPersonByIdAsync(Guid? guid);
        Task GetAllPersonWithSpAsync();
        Task<MemoryStream> GetPersonCSV();
    }
}
