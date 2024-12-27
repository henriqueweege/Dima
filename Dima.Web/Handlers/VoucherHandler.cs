using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handlers
{
    public class VoucherHandler(IHttpClientFactory factory) : IVoucherHandler
    {
        private readonly HttpClient client = factory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<Voucher?>> HandleAsync(GetVoucherByNumberRequest request)
        {
            return await client.GetFromJsonAsync<Response<Voucher?>>($"v1/vouchers/{request.Number}") ?? new Response<Voucher?>(null, 400, "Não foi possível complestar a chamada");
        }
    }
}
