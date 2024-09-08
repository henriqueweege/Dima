using Dima.Api.Common.Api;
using Dima.Api.Handlers.TransactionHandler;
using Dima.Core.Requests.Categories;
using Dima.Core.Models;
using Dima.Api.Endpoints.CRUDEndpoints;
using Dima.Core.Requests.Transactions;

namespace Dima.Api.Endpoints.TransactionEndpoints
{
    public class CreateTransactionEndpoint() 
        : CreateEndpoint<Transaction, CreateTransaction, UpdateTransaction, DeleteTransaction, GetByDateRangeTransaction, GetByIdTransaction>(EUrl.Transaction.ToString())
    {
    }
}
