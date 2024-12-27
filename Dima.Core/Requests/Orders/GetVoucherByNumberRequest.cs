using Dima.Core.Models;


namespace Dima.Core.Requests.Orders
{
    public class GetVoucherByNumberRequest : BaseRequest<Voucher>
    {
        public string Number { get; set; }
    }
}
