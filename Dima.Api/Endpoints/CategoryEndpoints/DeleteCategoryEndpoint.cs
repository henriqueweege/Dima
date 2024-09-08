using Dima.Api.Common.Api;
using Dima.Api.Handlers.CategoryHandler;
using Dima.Core.Requests.Categories;
using Dima.Core.Models;
using Dima.Api.Endpoints.CRUDEndpoints;

namespace Dima.Api.Endpoints.CategoryEndpoints
{
    public class DeleteCategoryEndpoint() : DeleteEndpoint<Category, CreateCategory, UpdateCategory, DeleteCategory, GetAllCategory, GetByIdCategory>(EUrl.Category.ToString())
    {
    }
}
