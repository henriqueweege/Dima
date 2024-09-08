using Dima.Core.Models;

namespace Dima.Core.Requests.Transactions
{
    public class GetByDateRangeTransaction : PagedRequest<Transaction>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
