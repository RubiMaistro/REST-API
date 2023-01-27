using BlazorWebClient.Services;
using Common.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWebClient.Pages
{
    public partial class PeopleList
    {
        [Inject]
        public IPersonService PersonService { get; set; }

        public List<Person> People { get; set; }

        protected override async Task OnInitializedAsync()
        {
            People = (await PersonService.GetPeople()).ToList();
        }
    }
}
