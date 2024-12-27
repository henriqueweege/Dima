using Dima.Core.Requests.Categories;
using Dima.Web.Handlers;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories
{
    public partial class EditCategories:ComponentBase
    {
        public bool IsBusy { get; set; } = false;
        [Parameter]
        public string Id { get; set; } = string.Empty;
        public UpdateCategory  InputModel { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }
        [Inject]
        public CategoryHandler  Handler { get; set; }

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var res = await Handler.Handle(Convert.ToInt32(Id), "");
                if (res.IsSuccess && res.Data is not null)
                {
                    InputModel = new UpdateCategory
                    {
                        Id = res.Data.Id,
                        Title = res.Data.Title,
                        Description = res.Data.Description
                    };
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy =false;
            }
            
        }

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var res = await Handler.Handle(InputModel);

                if (res.IsSuccess)
                {
                    Snackbar.Add("Categoria atualizada co msucesso", Severity.Info);
                    NavigationManager.NavigateTo("/categorias");
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
    }
}
