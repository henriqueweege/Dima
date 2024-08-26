using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers
{
    public class CategoryHandler : ICRUDHandler<Category, CreateCategory, UpdateCategory, DeleteCategory, GetAllCategory, GetByIdCategory>
    {
        private readonly AppDbContext _context;
        public CategoryHandler(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Response<Category?>> Handle(CreateCategory request)
        {
            try
            {
                Category category = Category.Create(request);

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return new Response<Category>(category, 200, "Categoria criada com sucesso.")!;

            }
            catch  
            {
                return new Response<Category>(null, 500, "Não foi possível criar a categoria.")!;
            }
        }

        public async Task<Response<Category?>> Handle(UpdateCategory request)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(x=> x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                {
                    return new Response<Category?>(null, 404, "Categoria para ser atualizada não foi encontrada.")!; 
                }
                
                category.Title = request.Title;
                category.Description = request.Description;
                
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();

                return new Response<Category>(category, 200, "Categoria atualizada com sucesso.")!;

            }
            catch  
            {
                return new Response<Category>(null, 500, "Não foi possível atualizar a categoria.")!;
            }
        }

        public async Task<Response<Category?>> Handle(DeleteCategory request)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(x=> x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                {
                    return new Response<Category?>(null, 404, "Categoria para ser excluída não foi encontrada.")!; 
                }
                
                
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return new Response<Category>(category, 204, "Categoria excluída com sucesso.")!;
            }
            catch  
            {
                return new Response<Category>(null, 500, "Não foi possível atualizar a categoria.")!;
            }
        }

        public async Task<PagedResponse<IEnumerable<Category?>>> Handle(GetAllCategory request)
        {
            try
            {
                var query = _context.Categories.Where(x => x.UserId == request.UserId).OrderBy(x => x.Title);
                
                var res =  query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
                
                var count = query.CountAsync();
                
                var x = await Task.WhenAll(res, count);
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return new Response<Category>(category, 204, "Categoria excluída com sucesso.")!;
            }
            catch  
            {
                return new Response<Category>(null, 500, "Não foi possível atualizar a categoria.")!;
            }
        }

        public Task<Response<Category>> Handle(GetByIdCategory request)
        {
            throw new NotImplementedException();
        }
    }
}
