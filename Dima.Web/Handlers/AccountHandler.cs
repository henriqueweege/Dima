﻿using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Core.Responses;
using System.Net.Http.Json;
using System.Text;

namespace Dima.Web.Handlers
{
    public class AccountHandler(IHttpClientFactory factory) : IAccountHandler
    {
        private readonly HttpClient _client = factory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<string>> LoginAsync(LoginRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/identity/login?UseCookies=true", request);
            return result.IsSuccessStatusCode 
                   ? new Response<string>("Login realizado com sucesso!", (int)result.StatusCode, "Login realizado com sucesso!") 
                   : new Response<string>(null, (int)result.StatusCode, "Não foi possível realizar o login.");
        }

        public async Task Logout()
        {
            var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
            await _client.PostAsJsonAsync("v1/identity/logout", emptyContent);
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/register", request);
            return result.IsSuccessStatusCode
                   ? new Response<string>("Cadastro realizado com sucesso!", (int)result.StatusCode, "Cadastro realizado com sucesso!")
                   : new Response<string>(null, (int)result.StatusCode, "Não foi possível realizar o login.");
        }
    }
}
