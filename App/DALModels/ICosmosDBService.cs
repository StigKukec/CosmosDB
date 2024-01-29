

using App.Models;

namespace App.DALModels
{
    public interface ICosmosDBService
    {
        Task<IEnumerable<VMPerson>> GetPeopleAsync(string queryString);
        Task<VMPerson?> GetPersonAsync(string idPerson);
        Task CreatePersonAsync(VMPerson person);
        Task UpdatePersonAsync(VMPerson person);
        Task DeletePersonAsync(VMPerson person);
    }
}
