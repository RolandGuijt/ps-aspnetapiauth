using Globomantics.Client.Models;

namespace Globomantics.Client.ApiServices
{
    public class ProposalApiService(HttpClient client) : IProposalApiService
    {
        public async Task<IEnumerable<ProposalModel>> GetAll(int conferenceId)
        {
            return await client.GetFromJsonAsync<IEnumerable<ProposalModel>>($"/proposal/all/{conferenceId}");
        }

        public async Task Add(ProposalModel model)
        {
            await client.PostAsJsonAsync("proposal", model);
        }

        public async Task<ProposalModel> Approve(int proposalId)
        {
            var resp =
                await client.PutAsync($"/proposal/approve/{proposalId}", null);
            return await resp.Content.ReadFromJsonAsync<ProposalModel>();
        }
    }
}
