using Dima.Core.Models;

namespace Dima.Core.Requests.Transactions
{
    public class DeleteTransaction : BaseRequest<Transaction>
    {
        public long Id { get; set; }
    }
}
