using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;

namespace Dima.Api.Handlers.TransactionHandler;

public interface ITransactionHandler : ICRUDHandler<Transaction, CreateTransaction, UpdateTransaction, DeleteTransaction, GetByDateRangeTransaction, GetByIdTransaction>
{
}
