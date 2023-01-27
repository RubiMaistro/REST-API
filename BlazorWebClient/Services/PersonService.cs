using Common.Models;

namespace BlazorWebClient.Services
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _httpClient;

        public PersonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            return await _httpClient.GetFromJsonAsync<Person[]>("person");
        }
    }
}
