using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Api.Handlers.TransactionHandler;
using Dima.Api.Endpoints.CRUDEndpoints;

namespace Dima.Api.Endpoints.TransactionEndpoints
{
    public class UpdateTransactionEndpoint()
        : UpdateEndpoint<Transaction, CreateTransaction, UpdateTransaction, DeleteTransaction, GetByDateRangeTransaction, GetByIdTransaction>(EUrl.Category.ToString())
    {

    }
}
