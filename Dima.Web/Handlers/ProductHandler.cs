using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handlers
{
    public class ProductHandler(IHttpClientFactory factory) : IProductHandler
    {
        private readonly HttpClient client = factory.CreateClient(Configuration.HttpClientName);

        public async Task<PagedResponse<List<Product?>>> HandleAsync(GetAllProductsRequest request)
        => await client.GetFromJsonAsync<PagedResponse<List<Product?>>>("v1/products") ?? new PagedResponse<List<Product?>>(null, 400, "Erro ao buscar produtos");
        public async Task<Response<Product?>> HandleAsync(GetProductBySlugRequest request)
                => await client.GetFromJsonAsync<Response<Product?>>($"v1/products/{request.Slug}") ?? new Response<Product?>(null, 400, "Erro ao buscar o produto");

    }
}
