using Dima.Core.Models;
using Dima.Core.Requests.Transactions;

namespace Dima.Core.Handlers;
public interface ITransactionHandler : ICRUDHandler<Transaction, CreateTransaction, UpdateTransaction, DeleteTransaction, GetByDateRangeTransaction>
{
}
