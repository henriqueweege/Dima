using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Transactions
{
    public partial class CreateTransactionPage : ComponentBase
    {
        public bool IsBusy { get; set; }
        public CreateTransaction InputModel { get; set; } = new();

        [Inject]
        public ITransactionHandler TransactionHandler { get; set; }

        [Inject]
        public ICategoryHandler CategoryHandler { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }
        public List<Category?> Categories { get; set; } = [];

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var result = await CategoryHandler.Handle(new GetAllCategory());

                if(result.IsSuccess)
                {
                    Categories = result.Data?.ToList() ?? [];
                    InputModel.CategoryId = Categories.FirstOrDefault()?.Id ?? 0; 
                }
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

        public async Task OnValidSubmitAsync()
        {
            try
            {
                var result = await TransactionHandler.Handle(InputModel);

                if (result.IsSuccess)
                    NavigationManager.NavigateTo("/lancamentos/historico");
                else
                    Snackbar.Add(result.Message, Severity.Error);
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
    }
}
