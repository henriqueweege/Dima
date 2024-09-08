using Dima.Core.Enums;
using Dima.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Transactions
{
    public class UpdateTransaction : BaseRequest<Transaction>
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Title must be provided.")]
        public string Title { get; set; } = default!;

        [Required(ErrorMessage = "PaidOrReceivedAt must be provided.")]
        public DateOnly? PaidOrReceivedAt { get; set; }
        
        [Required(ErrorMessage = "Type must be provided.")]
        public ETransactionType Type { get; set; }
        
        [Required(ErrorMessage = "Amount must be provided.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "CategoryId must be provided.")]
        public long CategoryId { get; set; }
    }
}
