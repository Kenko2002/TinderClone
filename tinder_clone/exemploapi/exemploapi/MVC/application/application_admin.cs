using Azure;
using example_db.Data;
using exemploapi.MVC.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace exemploapi.MVC.application
{
    public class FuncionalidadesDeAdmin
    {

        public static IResult DeleteAllData()
        {
            using (var dbContext = new MyProjectDbContext())
            {
                var allMatches = dbContext.Matches.ToList();
                var allUsers = dbContext.Users.ToList();
                var allAtracoes = dbContext.Atracoes.ToList();
                dbContext.Matches.RemoveRange(allMatches);
                dbContext.SaveChanges();
                dbContext.Atracoes.RemoveRange(allAtracoes);
                dbContext.SaveChanges();
                dbContext.Users.RemoveRange(allUsers);
                dbContext.SaveChanges();
                return Results.Ok("Todos os usuários foram deletados!");
            }


        }
        public static string Pikachu()
        {
            return "quu..__\r\n $$$b  `---.__\r\n  \"$$b        `--.                          ___.---uuudP\r\n   `$$b           `.__.------.__     __.---'      $$$$\"              .\r\n     \"$b          -'            `-.-'            $$$\"              .'|\r\n       \".                                       d$\"             _.'  |\r\n         `.   /                              ...\"             .'     |\r\n           `./                           ..::-'            _.'       |\r\n            /                         .:::-'            .-'         .'\r\n           :                          ::''\\          _.'            |\r\n          .' .-.             .-.           `.      .'               |\r\n          : /'$$|           .@\"$\\           `.   .'              _.-'\r\n         .'|$u$$|          |$$,$$|           |  <            _.-'\r\n         | `:$$:'          :$$$$$:           `.  `.       .-'\r\n         :                  `\"--'             |    `-.     \\\r\n        :##.       ==             .###.       `.      `.    `\\\r\n        |##:                      :###:        |        >     >\r\n        |#'     `..'`..'          `###'        x:      /     /\r\n         \\                                   xXX|     /    ./\r\n          \\                                xXXX'|    /   ./\r\n          /`-.                                  `.  /   /\r\n         :    `-  ...........,                   | /  .'\r\n         |         ``:::::::'       .            |<    `.\r\n         |             ```          |           x| \\ `.:``.\r\n         |                         .'    /'   xXX|  `:`M`M':.\r\n         |    |                    ;    /:' xXXX'|  -'MMMMM:'\r\n         `.  .'                   :    /:'       |-'MMMM.-'\r\n          |  |                   .'   /'        .'MMM.-'\r\n          `'`'                   :  ,'          |MMM<\r\n            |                     `'            |tbap\\\r\n             \\                                  :MM.-'\r\n              \\                 |              .''\r\n               \\.               `.            /\r\n                /     .:::::::.. :           /\r\n               |     .:::::::::::`.         /\r\n               |   .:::------------\\       /\r\n              /   .''               >::'  /\r\n              `',:                 :    .'\r\n                                   `:.:'";
        }

        public static IResult GetAllUsers()
        {
            using (var dbContext = new MyProjectDbContext())
            {
                var users = dbContext.Users
                                        .Include(u => u.genero)
                                         .Include(u => u.GenerosPelosQuaisSeAtrai)
                                         .ThenInclude(g => g.genero)
                                         .ToList();
                if (users.Count == 0)
                {
                    return Results.NotFound("Nenhum usuário encontrado.");
                }
                return Results.Ok(users);
            }
        }

        public static IResult GetAllMatches()
        {
            using (var dbContext = new MyProjectDbContext())
            {
                var matches = dbContext.Matches.ToList();
                if (matches.Count == 0)
                {
                    return Results.NotFound("Nenhum usuário encontrado.");
                }
                var response  = new List< List<User> >();
                foreach (var match in matches)
                {
                    var match_element = new List<User>();
                    
                    var user_requirinte = dbContext.Users.Find(match.user_requirinte_id);
                    var user_requirido = dbContext.Users.Find(match.user_requirido_id);
                    match_element.Add(user_requirinte);
                    match_element.Add(user_requirido);

                    response.Add(match_element);
                }

                return Results.Ok(response);
            }
        }


    }
}
