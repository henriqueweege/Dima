using Dima.Core.Enums;
using Dima.Core.Models.Base;
using Dima.Core.Requests.Transactions;

namespace Dima.Core.Models;

public class Transaction : BaseModel
{
    public string Title { get; set; } = default!;
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public DateOnly? PaidOrReceivedAt { get; set; }

    public ETransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public string UserId { get; set; } = default!;

    public static Transaction Create(CreateTransaction request)
        => new Transaction { Title = request.Title, PaidOrReceivedAt = request.PaidOrReceivedAt, Type = request.Type, Amount = request.Amount, CategoryId = request.CategoryId, UserId = request.UserId };
}

