using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handlers
{
    public class CategoryHandler(IHttpClientFactory factory) : ICategoryHandler
    {
        private readonly HttpClient _client = factory.CreateClient(Configuration.HttpClientName);
        public async Task<Response<Category?>> Handle(CreateCategory request)
        {
            var result =await  _client.PostAsJsonAsync("v1/categories", request);

            return await result.Content.ReadFromJsonAsync<Response<Category?>>() ?? new Response<Category?>(null, 400, "falha ao criar as categoria");
        }

        public async Task<Response<Category?>> Handle(UpdateCategory request)
        {
            var result = await _client.PutAsJsonAsync($"v1/categories/{request.Id}", request);

            return await result.Content.ReadFromJsonAsync<Response<Category?>>() ?? new Response<Category?>(null, 400, "falha ao atualizar as categoria");
        }

        public async Task<Response<Category?>> Handle(DeleteCategory request)
        {
            var result = await _client.DeleteAsync($"v1/categories/{request.Id}");

            return await result.Content.ReadFromJsonAsync<Response<Category?>>() ?? new Response<Category?>(null, 400, "falha ao deletar as categoria");
        }

        public async Task<PagedResponse<IEnumerable<Category?>>> Handle(GetAllCategory request)
        {
            var result = await _client.GetFromJsonAsync<PagedResponse<IEnumerable<Category?>>>($"v1/categories");

            return result ?? new PagedResponse<IEnumerable<Category?>>(null, 400, "falha ao recuperar as categoria");
        }

        public async Task<Response<Category>> Handle(long id, string email)
        {
            var result = await _client.GetFromJsonAsync<Response<Category>>($"v1/categories/{id}");

            return result ?? new Response<Category?>(null, 400, "falha ao recuperar as coisas");
        }

    }
}
