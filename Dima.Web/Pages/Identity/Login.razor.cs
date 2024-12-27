using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Web.Handlers;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Dima.Web.Pages.Identity
{
    public partial class LoginPage : ComponentBase
    {
        [Inject]
        public ISnackbar SnackBar { get; set; } = null!;

        [Inject]
        public IAccountHandler Handler { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

        public LoginRequest InputModel { get; set; } = new();
        public bool IsBusy { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is { IsAuthenticated: true })
            {
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                AuthenticationStateProvider.NotifyAuthenticationStateChanged();
                NavigationManager.NavigateTo("/");
            }
        }

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;
            try
            {
                var result = await Handler.LoginAsync(InputModel);

                if (result.IsSuccess)
                {
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    SnackBar.Add(result.Message, Severity.Error);
                }
            }
            catch (Exception ex)
            {
                SnackBar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
