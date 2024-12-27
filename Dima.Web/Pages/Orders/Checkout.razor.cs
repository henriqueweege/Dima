using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Orders
{
    public partial class CheckoutPage :ComponentBase
    {

        public PatternMask Mask = new PatternMask("####-####")
        {
            MaskChars = [new MaskChar('#', @"[0-9a-fA-F]")],
            Placeholder = '_',
            CleanDelimiters = true,
            Transformation= (char c) => c.ToString().ToUpperInvariant()[0]
        };
        [Parameter]
        public string ProductSlug { get; set; } = string.Empty;

        [SupplyParameterFromQuery(Name = "voucher")]
        public string? VoucherNumber { get; set; } = string.Empty;

        public bool IsBusy { get; set; }
        public bool IsValid { get; set; }

        public CreateOrderRequest InputModel { get; set; }
        public Product? Product { get; set; }
        public Voucher? Voucher { get; set; }
        public decimal Total { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IProductHandler ProductHandler { get; set; }

        [Inject]
        public IOrderHandler OrderHandler { get; set; }

        [Inject]
        public IVoucherHandler VoucherHandler { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;
            try
            {
                var res = await OrderHandler.HandleAsync(new CreateOrderRequest() { ProductId = Product!.Id, VoucherId = Voucher?.Id ?? null });

                if (res.IsSuccess)
                    NavigationManager.NavigateTo($"/pedidos/{res.Data!.Number}");
                else
                    Snackbar.Add(res.Message, Severity.Error);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var res = await ProductHandler.HandleAsync(new GetProductBySlugRequest() { Slug = ProductSlug });
                if (!res.IsSuccess)
                {
                    Snackbar.Add("Não foi possível obter o produto", Severity.Error);
                    return;
                }

                Product = res.Data;

            }
            catch
            {
                Snackbar.Add("Não foi possível obter o produto", Severity.Error);
                IsBusy = false;
                return;
            }


            if(Product is null)
            {
                Snackbar.Add("Não foi possível obter o produto", Severity.Error);
                IsBusy = false;
                return;
            }

            if (!string.IsNullOrWhiteSpace(VoucherNumber))
            {
                try
                {
                    var res = await VoucherHandler.HandleAsync(new GetVoucherByNumberRequest() { Number = VoucherNumber.Replace("-", "") });

                    if (!res.IsSuccess || res.Data is null)
                    {
                        Snackbar.Add("Não foi possível obter o voucher", Severity.Error);
                        IsBusy = false;
                        return;
                    }

                    Voucher = res.Data;
                }
                catch (Exception)
                {
                    Snackbar.Add("Não foi possível obter o voucher", Severity.Error);
                    IsBusy = false;
                    return;
                }
            }
            IsValid = true;
            Total = Product.Price - (Voucher?.Amount ?? 0);
        }
    }
}
