
using Dima.Core.Models.Base;

namespace Dima.Core.Models;

public class Product : BaseModel
{
    public string Title { get; set; }
    public string Desciption { get; set; }
    public string Slug { get; set; }
    public bool IsActive { get; set; }
    public decimal Price { get; set; }
}
