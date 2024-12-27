
using Dima.Core.Enums;
using Dima.Core.Models.Base;

namespace Dima.Core.Models;

public class Order : BaseModel
{
    public string Number { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? ExternalReference { get; set; }
    public EPaymentGateway Gateway { get; set; } = EPaymentGateway.Stripe;
    public EOrderStatus Status { get; set; } = EOrderStatus.WaitingPayment;
    public string UserId { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public long? VoucherId { get; set; }
    public Voucher? Voucher { get; set; }
    public decimal Total => Product.Price - (Voucher?.Amount ?? 0);

}
