using Dima.Core.Models;

namespace Dima.Core.Requests.Categories;

public class DeleteCategory : BaseRequest<Category>
{
    public long Id { get; set; }
}
