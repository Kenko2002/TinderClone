using controller_example.Controllers;
using Microsoft.Extensions.DependencyInjection;
using example_db.Data;
using ModelExample.Models;
using exemploapi.MVC.controller;

var builder = WebApplication.CreateBuilder(args);


var context = new MyProjectDbContext();




// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSwaggerGen();
builder.Services.AddSession(options =>
    {
        // Configurações de opções de sessão
        options.Cookie.Name = ".Tinder.Session";
        options.IdleTimeout = TimeSpan.FromMinutes(30); // Define o tempo limite de inatividade da sessão
                                                        // Outras configurações, se necessário
    });




var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSession();

app.UseHttpsRedirection();

app.AdicionarControllersUser();
app.AdicionarControllersAdmin();

app.Run();

//LEMBRAR DE IMPLEMENTAR O CONCEITO DE PERFIL DE USUÁRIO
//para verificar se o usuário logado tem acesso ao controller que ele tentou acessar
