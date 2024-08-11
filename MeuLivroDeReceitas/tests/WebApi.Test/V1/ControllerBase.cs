using MeuLivroDeReceitas.API.Controllers;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Excepetions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace WebApi.Test.V1;

public class ControllerBase : IClassFixture<MeuLivroDeReceitasWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ControllerBase(MeuLivroDeReceitasWebApplicationFactory<Program> factory)
    {
        
        _client = factory.CreateClient();
        ResourceMensagensDeErro.Culture = CultureInfo.CurrentCulture;
    }

    protected  async Task <HttpResponseMessage>  PostRequest (string metodo, object body,string token = "")
    {
    
        var jsonString = JsonConvert.SerializeObject(body);
     
        return await _client.PostAsync(metodo, new StringContent(jsonString, Encoding.UTF8,"application/json"));
    }

    protected async Task<HttpResponseMessage> PutRequest(string metodo, object body, string token = "")
    {
       
        var jsonString = JsonConvert.SerializeObject(body);

        return await _client.PutAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }

    public async Task<string> Login(string email, string senha)
    {
        var request = new RequestLoginJson
        {
            Email = email,
            Senha = senha
        };

        var resposta = await PostRequest("login", request);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(respostaBody);

       
        var token = responseData.RootElement.GetProperty("token").GetString();
        AutorizarRequest(token);

        return token;
    }



    private void AutorizarRequest(string token)
    {
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }
}
