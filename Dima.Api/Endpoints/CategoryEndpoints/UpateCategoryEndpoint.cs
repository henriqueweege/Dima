using Dima.Core.Requests.Categories;
using Dima.Core.Models;
using Dima.Api.Endpoints.CRUDEndpoints;

namespace Dima.Api.Endpoints.CategoryEndpoints
{
    public class UpdateCategoryEndpoint() : UpdateEndpoint<Category, CreateCategory, UpdateCategory, DeleteCategory, GetAllCategory, GetByIdCategory>(EUrl.Category.ToString())
    {
    }
}
