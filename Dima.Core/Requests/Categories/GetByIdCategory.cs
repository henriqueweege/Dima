using Dima.Core.Models;

namespace Dima.Core.Requests.Categories;

public class GetByIdCategory : BaseRequest<Category>
{
    public long Id { get; set; }
}
