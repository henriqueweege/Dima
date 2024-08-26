using Dima.Core.Requests.Categories;

namespace Dima.Core.Models;

public class Category
{
    public long Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string UserId { get; set; } = default!;

    public static Category Create(CreateCategory request)
    {
        throw new NotImplementedException();
    }
}
