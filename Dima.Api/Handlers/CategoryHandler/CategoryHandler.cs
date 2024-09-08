using Dima.Api.Data;
using Dima.Core.Models;
using Dima.Core.Requests;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers.CategoryHandler;

public class CategoryHandler : ICategoryHandler
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
            request.UserId = "string";
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
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

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
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

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

            var res = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            var count = await query.CountAsync();


            return new PagedResponse<IEnumerable<Category?>>(request.PageNumber, request.PageSize, count, res, 200, "Categorias consultadas com sucesso.")!;
        }
        catch
        {
            return new PagedResponse<IEnumerable<Category?>>(0, 0, 0, null, 500, "Não foi possível consultar categorias.")!;
        }
    }

    public async Task<Response<Category>> Handle(BaseGetById<Category> request)
    {
        try
        {

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null)
            {
                return new Response<Category?>(null, 404, "Categoria para ser excluída não foi encontrada.")!;
            }


            return new Response<Category>(category, 200, "Categoria consultada com sucesso.")!;
        }
        catch
        {
            return new Response<Category>(null, 500, "Não foi possível consultar a categoria.")!;
        }
    }
}
