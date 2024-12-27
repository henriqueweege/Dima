using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories
{
    public partial class ListCategoriesPage:ComponentBase
    {
        public bool IsBusy { get; set; } = false;
        public List<Category?> Categories { get; set; } = [];
        public string SearchTerm { get; set; } = string.Empty;

        [Inject]
        public ICategoryHandler Handler { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;

            try
            {
                var request = new GetAllCategory();
                var result = await Handler.Handle(request);

                if (result.IsSuccess)
                    Categories = result.Data.ToList() ?? [];
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

        public Func<Category, bool> Filter => x =>
        {
            return string.IsNullOrEmpty(SearchTerm) ||
            x.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
            x.Title.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
            (x.Description is not null && x.Description.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
        };

        public async void OnDeleteButtonClickedAsync(long id, string title)
        {
            var result = await DialogService.ShowMessageBox("ATENçÃO", $"Ao prosseguior a categoria {title} será excluída. Deseja continuar?", yesText: "EXCLUIR", noText:"CANCELAR");

            if (result is true)
            {
                await OnDeleteAsync(id, title);
            }

            StateHasChanged();
        }

        public async Task OnDeleteAsync(long id, string title)
        {
            IsBusy = true;
            try
            {

                await Handler.Handle(new DeleteCategory() { Id = id });
                Categories.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Categoria {title} removida com sucesso.", Severity.Info);
            }
            catch (Exception)
            {
                Snackbar.Add($"Algum erro ocorreu excluindo categoria {title}.", Severity.Error);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
