using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;

namespace Dima.Core.Handlers;
public interface ICategoryHandler : ICRUDHandler<Category, CreateCategory, UpdateCategory, DeleteCategory, GetAllCategory>
{
}
