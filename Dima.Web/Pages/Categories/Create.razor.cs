using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories
{
    public partial class CreateCategoryPage : ComponentBase
    {
        public bool IsBusy { get; set; }
        public CreateCategory InputModel { get; set; } = new();

        [Inject]
        public ICategoryHandler  Handler  { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        [Inject]
        public ISnackbar Snackbar { get; set; }

        public async Task OnValidSubmitAsync()
        {
            try
            {
                var result = await Handler.Handle(InputModel);

                if (result.IsSuccess)
                    NavigationManager.NavigateTo("/categorias");
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
