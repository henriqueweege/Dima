using Dima.Core.Models;
using Dima.Api.Endpoints.CRUDEndpoints;
using Dima.Core.Requests.Transactions;

namespace Dima.Api.Endpoints.TransactionEndpoints;

public class DeleteTransactionEndpoint() 
    : DeleteEndpoint<Transaction, CreateTransaction, UpdateTransaction, DeleteTransaction, GetByDateRangeTransaction, GetByIdTransaction>(EUrl.Transaction.ToString())
{
    
}
