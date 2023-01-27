using Common.Models;

namespace BlazorWebClient.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetPeople();
    }
}
