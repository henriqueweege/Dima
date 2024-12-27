using Dima.Core.Handlers;
using Dima.Core.Requests.Stripe;
using Dima.Core.Responses;
using Dima.Core.Responses.Stripe;
using System.Net.Http.Json;

namespace Dima.Web.Handlers
{
    public class StripeHandler(IHttpClientFactory factory) : IStripeHandler
    {
        private readonly HttpClient client = factory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<string?>> HandleAsync(CreateSessionRequest request)
        {
            var res = await client.PostAsJsonAsync($"v1/payments/session", request);

            return await res.Content.ReadFromJsonAsync<Response<string?>>() ?? new Response<string?>(null, 400, "Falha ao criar sessão no Stripe");
        }

        public async Task<Response<List<StripeTransactionResponse>?>> HandleAsync(GetTransactionsByOrderNumberRequest request)
        {
            var res = await client.PostAsJsonAsync($"v1/payments/stripe/{request.Number}/transactions", request);

            return await res.Content.ReadFromJsonAsync<Response<List<StripeTransactionResponse>?>>() ?? new Response<List<StripeTransactionResponse>?>(null, 400, "Falha ao criar sessão no Stripe");
        }
    }
}
