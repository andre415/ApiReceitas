using MeuLivroDeReceitas.API.Filtros;
using MeuLivroDeReceitas.API.Filtros.UsuarioAutenticado;
using MeuLivroDeReceitas.API.WebSockets;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Application.Servicos.AutoMapper;
using MeuLivroDeReceitas.Domain.Extensions;
using MeuLivroDeReceitas.Infrastructure;
using MeuLivroDeReceitas.Infrastructure.AcessoRepo;
using MeuLivroDeReceitas.Infrastructure.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var conexao = builder.Configuration.GetConexao();
var nomeDataBase = builder.Configuration.GetnomeDataBase();
// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddRouting(option => option.LowercaseUrls = true);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositorio(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(options => options.Filters.Add(typeof(FiltroDasExceptions)));
builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfig());
}).CreateMapper());

builder.Services.AddScoped<IAuthorizationHandler, UsuarioLogadoHandler>();
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("UsuarioLogado", policy => policy.Requirements.Add(new UsuarioLogadoRequirement()));
});

builder.Services.AddScoped<UsuarioAutenticadoAttribute>();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();


atualizarDataBase();
app.MapHub<AdicionarConexao>("/addConexao");
app.Run();



void atualizarDataBase()
{
    using var servicesScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    using var context = servicesScope.ServiceProvider.GetService<MeuLivroDeReceitasContext>();

    bool? dataBaseInMemory = context?.Database?.ProviderName?.Equals("Microsoft.EntityFrameworkCore.InMemory");
    if (!dataBaseInMemory.HasValue || !dataBaseInMemory.Value)
    {
        var nomeDataBase = builder.Configuration.GetnomeDataBase();
        DataBase.CriarDataBase(conexao, nomeDataBase);

        app.MigrarBancoDeDados();
    }
    
}

public partial class Program { }