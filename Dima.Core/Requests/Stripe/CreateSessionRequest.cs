using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Stripe
{
    public class CreateSessionRequest : BaseRequest<string>
    {
        public string OrderNumber { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        public long OrderTotal { get; set; }
    }
}
