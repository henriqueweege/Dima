using Dima.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class UpdateCategory : BaseRequest<Category>
{
    [Required(ErrorMessage = "Title must be provided.")]
    [MaxLength(80, ErrorMessage = "Title must not be greater than 80 characters.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description must be provided.")]
    public string Description { get; set; }
}
