using Dima.Core.Handlers;
using Dima.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Products
{
    public partial class ListPage : ComponentBase
    {
        private bool IsBusy { get; set; }
        public List<Product> Products { get; set; } = [];

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IProductHandler Handler { get; set; }

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var res = await Handler.HandleAsync(new Core.Requests.Orders.GetAllProductsRequest());

                if (res.IsSuccess)
                {
                    Products = res.Data;
                }
                else
                {
                    Snackbar.Add(res.Message, Severity.Error);
                }
            }
            catch(Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
