using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Transactions
{
    public partial class EditTransaction:ComponentBase
    {
        public bool IsBusy { get; set; }
        public UpdateTransaction InputModel { get; set; } = new();

        [Parameter]
        public string Id { get; set; } = string.Empty;

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
            await GetTransactionByIdAsync();
            await GetCategoriesAsync();
            IsBusy = false;
        }

        private async Task GetTransactionByIdAsync()
        {
            IsBusy = true;
            try
            {
                var result = await TransactionHandler.Handle(long.Parse(Id), string.Empty);

                if (result.IsSuccess && result.Data is not null)
                {
                    InputModel = UpdateTransaction.Create(result.Data);
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
        private async Task GetCategoriesAsync()
        {
            IsBusy = true;
            try
            {
                var result = await CategoryHandler.Handle(new GetAllCategory());

                if (result.IsSuccess)
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
