using Dima.Core.Models;


namespace Dima.Core.Requests.Orders
{
    public class GetProductBySlugRequest : BaseRequest<Product>
    {
        public string Slug { get; set; }
    }
}
