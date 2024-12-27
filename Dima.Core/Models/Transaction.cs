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
        => new Transaction { Title = request.Title, PaidOrReceivedAt = new DateOnly(request.PaidOrReceivedAt.Value.Date.Year, request.PaidOrReceivedAt.Value.Date.Month, request.PaidOrReceivedAt.Value.Date.Day), Type = request.Type, Amount = GetNormalizedAmount(request.Amount, request.Type), CategoryId = request.CategoryId, UserId = request.UserId };

    public static decimal GetNormalizedAmount(decimal amount, ETransactionType type) => (type == ETransactionType.Deposit || amount == decimal.Zero) ? Math.Abs(amount) : Math.Abs(amount) * -1;
}

