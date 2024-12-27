using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers
{
    public class ProductHandler(AppDbContext context) : IProductHandler
    {
        public async Task<PagedResponse<List<Product?>>> HandleAsync(GetAllProductsRequest request)
        {
            try
            {
                var query = context.Products.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Title);

                var products = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();


                var count = await query.CountAsync();

                return new PagedResponse<List<Product?>>(request.PageNumber, request.PageSize, count, products, 200);
            }
            catch
            {
                return new PagedResponse<List<Product?>>(0, 0, 0, null, 500, "não foi possível consultar os produtos" );
            }
        }

        public async Task<Response<Product?>> HandleAsync(GetProductBySlugRequest request)
        {
            try
            {
                var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.IsActive && x.Slug == request.Slug);

                return product is null ? new Response<Product?>(null, 404, "producto não encontrado") : new Response<Product?>(product);
            }
            catch
            {
                return new Response<Product?>(null, 500, "não foi possível recuperar o seu produto");
            }
        }
    }
}
