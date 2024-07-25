using exemploapi.MVC.application;

namespace exemploapi.MVC.controller
{
    static class AdminController
    {

        public static void AdicionarControllersAdmin(this WebApplication app)
        {
            app.MapGet("/pikachu", FuncionalidadesDeAdmin.Pikachu);
            app.MapPost("/deletealldata", FuncionalidadesDeAdmin.DeleteAllData);
            app.MapGet("/users/getall", FuncionalidadesDeAdmin.GetAllUsers);
            app.MapGet("/matches/getall", FuncionalidadesDeAdmin.GetAllMatches);
        }

    }
}
