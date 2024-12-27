using Dima.Core.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handlers
{
    public class TransactionHandler(IHttpClientFactory factory) : ITransactionHandler
    {
        private readonly HttpClient _client = factory.CreateClient(Configuration.HttpClientName);
        public async Task<Response<Transaction?>> Handle(CreateTransaction request)
        {
            var result = await _client.PostAsJsonAsync("v1/transactions", request);

            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>() ?? new Response<Transaction?>(null, 400, "falha ao criar transação");
        }

        public  async Task<Response<Transaction?>> Handle(UpdateTransaction request)
        {
            var result = await _client.PutAsJsonAsync("v1/transactions", request);

            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>() ?? new Response<Transaction?>(null, 400, "falha ao atualizar transação");
        }

        public async  Task<Response<Transaction?>> Handle(DeleteTransaction request)
        {
            var result = await _client.DeleteAsync($"v1/categories/{request.Id}");

            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>() ?? new Response<Transaction?>(null, 400, "falha ao atualizar transação");
        }

        public async  Task<PagedResponse<IEnumerable<Transaction?>>> Handle(GetByDateRangeTransaction request)
        {
            const string format = "yyyy-MM-dd";
            
            var startDate = request.StartDate is not null ? request.StartDate.Value.ToString(format) : DateTime.Now.GetStartDay().ToString(format);
            var endDate = request.EndDate is not null ? request.EndDate.Value.ToString(format) : DateTime.Now.GetEndDay().ToString(format);

            var url = $"v1/transactions?startDate{startDate}&endDate={endDate}";

            return await _client.GetFromJsonAsync<PagedResponse<IEnumerable<Transaction?>>>(url) ?? new PagedResponse<IEnumerable<Transaction?>>(null, 400, "falha ao buscar transações");
        }

        public async  Task<Response<Transaction>> Handle(long id, string email)
        {
            var result = await _client.GetFromJsonAsync<Response<Transaction>>($"v1/transactions/{id}");

            return result ?? new Response<Transaction?>(null, 400, "falha ao recuperar as transação");
        }
    }
}
