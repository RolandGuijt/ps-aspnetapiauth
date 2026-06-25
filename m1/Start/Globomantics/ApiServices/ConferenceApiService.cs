using Globomantics.Client.Models;

namespace Globomantics.Client.ApiServices
{
    public class ConferenceApiService(HttpClient client) : IConferenceApiService
    {
        public async Task<IEnumerable<ConferenceModel>> GetAll()
        {
            return await client
                .GetFromJsonAsync<IEnumerable<ConferenceModel>>("/conference");
        }

        public async Task<ConferenceModel> GetById(int id)
        {
            return await client
                .GetFromJsonAsync<ConferenceModel>($"/conference/{id}");
        }

        public async Task Add(ConferenceModel model)
        {
            await client.PostAsJsonAsync("/conference", model);
        }
    }
}
