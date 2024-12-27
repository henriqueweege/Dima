using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class RegisterPage : ComponentBase
{
    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;
    
    [Inject]
    public IAccountHandler AccountHandler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject]
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    public RegisterRequest InputModel { get; set; } = new();
    public bool IsBusy { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if(user.Identity is { IsAuthenticated: true })
        {
            NavigationManager.NavigateTo("/");
        }
    }

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await AccountHandler.RegisterAsync(InputModel);

            if (result.IsSuccess)
            {
                SnackBar.Add(result.Message, Severity.Success);
                NavigationManager.NavigateTo("/login");
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
