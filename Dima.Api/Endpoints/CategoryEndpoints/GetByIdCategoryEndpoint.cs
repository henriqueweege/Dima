using Dima.Api.Common.Api;
using Dima.Api.Handlers.CategoryHandler;
using Dima.Core.Models;
using Dima.Api.Endpoints.CRUDEndpoints;
using Dima.Core.Requests.Categories;

namespace Dima.Api.Endpoints.CategoryEndpoints
{
    public class GetByIdCategoryEndpoint() : GetByIdEndpoint<Category, CreateCategory, UpdateCategory, DeleteCategory, GetAllCategory, GetByIdCategory>(EUrl.Category.ToString())
    {
    }
}
