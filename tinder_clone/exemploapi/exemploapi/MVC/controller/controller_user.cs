using example_db.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using exemploapi.MVC.model;
using exemploapi.MVC.application;


namespace exemploapi.MVC.controller
{

    static class UserController
    {

        public static void AdicionarControllersUser(this WebApplication app)
        {
            app.MapPost("/user/create", FuncionalidadesDeUsuario.UserCreate);
            app.MapGet("/user/getall", FuncionalidadesDeUsuario.UserGetAll);
            app.MapGet("/user/getbyid", FuncionalidadesDeUsuario.UserById);
            app.MapPost("/user/deletebyid", FuncionalidadesDeUsuario.DeleteById);
            app.MapPut("/user/updatemyuser", FuncionalidadesDeUsuario.UpdateMyUser);
            app.MapPost("/user/login", FuncionalidadesDeUsuario.Logar);
            app.MapGet("/user/getmyuser", FuncionalidadesDeUsuario.MyUser);
            app.MapPost("/user/sendnewmatch", FuncionalidadesDeUsuario.NovaRequisicaoMatch);
            app.MapGet("/user/mymatches", FuncionalidadesDeUsuario.BuscarMatchesDoUsuario);
            app.MapPost("/user/aceitarmatch", FuncionalidadesDeUsuario.AceitarMatch);
            app.MapPost("/user/recusarmatch", FuncionalidadesDeUsuario.RecusarMatch);


        }

    }
}



