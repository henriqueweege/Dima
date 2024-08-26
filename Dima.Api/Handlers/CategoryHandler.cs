using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Handlers
{
    public class CategoryHandler : ICRUDHandler<Category, CreateCategory, UpdateCategory, DeleteCategory, GetAllCategory, GetByIdCategory>
    {
        private readonly AppDbContext _context;
        public CategoryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<Category>> Create(CreateCategory request)
        {
            try
            {
                Category category = Category.Create(request);

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return new Response<Category>(category);
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public Task<Response<Category>> Delete(DeleteCategory request)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<IEnumerable<Category>>> GetAll(GetAllCategory request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Category>> GetById(GetByIdCategory request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Category>> Update(UpdateCategory request)
        {
            throw new NotImplementedException();
        }
    }
}
