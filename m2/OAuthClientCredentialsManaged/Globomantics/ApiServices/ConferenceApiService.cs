using Globomantics.Shared;

namespace Globomantics.ApiServices
{
    public class ConferenceApiService(IHttpClientFactory factory) : IConferenceApiService {
        private readonly HttpClient _Client = factory.CreateClient("globoapi");

        public async Task<IEnumerable<ConferenceModel>> GetAll()
        {
            return await _Client
                .GetFromJsonAsync<IEnumerable<ConferenceModel>>("/conference");
        }

        public async Task<ConferenceModel> GetById(int id)
        {
            return await _Client
                .GetFromJsonAsync<ConferenceModel>($"/conference/{id}");
        }

        public async Task Add(ConferenceModel model)
        {
            await _Client.PostAsJsonAsync("/conference", model);
        }
    }
}

