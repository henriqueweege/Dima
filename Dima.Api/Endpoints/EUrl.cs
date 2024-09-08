using System.ComponentModel;

namespace Dima.Api.Endpoints
{
    public enum EUrl
    {
        [Description($"/category")]
        Category,
        [Description("/transaction")]
        Transaction
    }
}
