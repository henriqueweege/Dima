using Dima.Api.Data;
using Dima.Core.Extensions;
using Dima.Core.Models;
using Dima.Core.Requests;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers.TransactionHandler;

public class TransactionHandler : ITransactionHandler
{
    private readonly AppDbContext _context;
    public TransactionHandler(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Response<Transaction?>> Handle(CreateTransaction request)
    {
        try
        {
            Transaction transaction = Transaction.Create(request);

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso.");

        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Não foi possível criar a transação.");
        }
    }

    public async Task<Response<Transaction?>> Handle(UpdateTransaction request)
    {
        try
        {
            var getTransactionToUpdate = await Handle(new GetByIdTransaction() { Id = request.Id, UserId = request.UserId });

            if (!getTransactionToUpdate.IsSuccess)
            {
                return new Response<Transaction?>(null, 404, "Transaction para ser atualizar não foi encontrada.")!;
            }

            var toUpdate = getTransactionToUpdate.Data!;

            toUpdate.Title = request.Title;
            toUpdate.CategoryId = request.CategoryId;
            toUpdate.Amount = request.Amount;
            toUpdate.PaidOrReceivedAt = request.PaidOrReceivedAt;
            toUpdate.Type = request.Type;

            _context.Transactions.Update(toUpdate);
            await _context.SaveChangesAsync();

            return new Response<Transaction?>(toUpdate, 200, "Transação atualizada com sucesso.");


        }
        catch
        {

            return new Response<Transaction?>(null, 500, "Não foi possível atualizar a transação.");
        }
    }

    public async Task<Response<Transaction?>> Handle(DeleteTransaction request)
    {
        try
        {
            var toRemove = await Handle(new GetByIdTransaction() { Id = request.Id, UserId = request.UserId });

            if (!toRemove.IsSuccess)
            {
                return new Response<Transaction?>(null, 404, "Transaction para ser remover não foi encontrada.")!;
            }

            _context.Transactions.Remove(toRemove.Data!);
            await _context.SaveChangesAsync();

            return new Response<Transaction>(toRemove.Data, 204, "Categoria excluída com sucesso.")!;

        }
        catch 
        {

            return new Response<Transaction?>(null, 500, "Não foi possível deletar a transação.");
        }
    }

    public async Task<PagedResponse<IEnumerable<Transaction?>>> Handle(GetByDateRangeTransaction request)
    {

        try
        {
            request.StartDate ??= DateTime.Now.GetStartDay();
            request.EndDate ??= DateTime.Now.GetEndDay();

            var query = _context.Transactions.Where(x => x.UserId == request.UserId && x.CreatedAt >= request.StartDate && x.CreatedAt <= request.EndDate).OrderBy(x => x.Title);

            var res = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            var count = await query.CountAsync();


            return new PagedResponse<IEnumerable<Transaction?>>(request.PageNumber, request.PageSize, count, res, 200, "Categorias consultadas com sucesso.")!;
        }
        catch
        {
            return new PagedResponse<IEnumerable<Transaction?>>(0, 0, 0, null, 500, "Não foi possível consultar categorias.")!;

        }
    }

    public async Task<Response<Transaction>> Handle(BaseGetById<Transaction> request)
    {
        try
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (transaction is null)
            {
                return new Response<Transaction?>(null, 404, "Transação para ser excluída não foi encontrada.")!;
            }


            return new Response<Transaction>(transaction, 200, "Transação consultada com sucesso.")!;
        }
        catch
        {
            return new Response<Transaction>(null, 500, "Não foi possível consultar a transação.")!;
        }
    }
}
