using Dima.Core.Models.Base;
using Dima.Core.Requests.Categories;

namespace Dima.Core.Models;

public class Category : BaseModel
{ 
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string UserId { get; set; } = default!;

    public static Category Create(CreateCategory request)
        => new Category { Title = request.Title, Description = request.Description, UserId = request.UserId };
}
