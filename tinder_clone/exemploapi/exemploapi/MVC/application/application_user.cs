using example_db.Data;
using exemploapi.MVC.DTO;
using exemploapi.MVC.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace exemploapi.MVC.application
{
    public class FuncionalidadesDeUsuario
    {
        public static IResult UpdateMyUser(HttpContext httpContext,[FromBody] UserDTO post_content)
        {
            User post = new User();
            post.transferirInformacao_DTO(post_content);
            

            using (var dbContext = new MyProjectDbContext())
            {
                if (httpContext.Session.GetInt32("User_Id") != null)
                {
                    post.Id = httpContext.Session.GetInt32("User_Id").Value;
                }
                else
                {
                    return Results.Ok("Você não está logado");
                }

                //conferindo se o id já existe na base de dados
                var user = dbContext.Users.FirstOrDefault(u => u.Id == post.Id);
                dbContext.Entry(user).CurrentValues.SetValues(post);

                dbContext.SaveChanges();
                return Results.Ok(post);
            }

        }

        public static IResult UserCreate([FromBody] UserDTO post_content)
        {
            User post = new User();
            post.transferirInformacao_DTO(post_content);
            using (var dbContext = new MyProjectDbContext())
            {
                //conferindo se o usuário já existe na base de dados
                var users = dbContext.Users.Where(u => u.nome == post.nome).ToList();
                if (users.Count != 0)
                {
                    return Results.Ok("Esse user já está cadastrado!");
                }

                //conferindo se junto do usuário foi passado um gênero para associar a ele.
                if (post.genero.Id != 0)
                {
                    post.genero = dbContext.Generos.Find(post.genero.Id);
                    if (post.genero == null) { return Results.Ok("Gênero não encontrado"); }
                }

                //conferindo se junto do usuário foi passado uma lista de gêneros pelos quais ele se atrai pra associar a ele.
                foreach (var atracao in post.GenerosPelosQuaisSeAtrai)
                {
                    if (atracao.genero.Id != 0)
                    {
                        atracao.genero = dbContext.Generos.Find(atracao.genero.Id);
                        if (atracao.genero == null) { return Results.Ok("Atração não encontrada"); }
                    }
                }

                dbContext.Users.Add(post);
                
                dbContext.SaveChanges();
                return Results.Ok(post);
            }
        }

        public static IResult UserGetAll()
        {
            using (var dbContext = new MyProjectDbContext())
            {
                //var users = dbContext.Users.ToList();
                var users = dbContext.Users
                                            .Include(u => u.genero)
                                             .Include(u => u.GenerosPelosQuaisSeAtrai)
                                             .ThenInclude(g => g.genero)
                                             .ToList();
                if (users.Count == 0)
                {
                    return Results.NotFound("Nenhum usuário encontrado.");
                }

                //escondendo dados sensíveis usando um DTO.
                var response = new List<UserDTO>();
                users.ForEach(u =>
                    {
                        var userdto = new UserDTO();
                        userdto.nome = u.nome;
                        userdto.genero = u.genero;
                        userdto.descricaoPessoal = u.descricaoPessoal;
                        userdto.dataNascimento = u.dataNascimento;
                        userdto.GenerosPelosQuaisSeAtrai = u.GenerosPelosQuaisSeAtrai;
                        response.Add(userdto);
                    });
                
                return Results.Ok(response);
            }
            
        }

        public static IResult UserById(HttpContext httpContext, [FromQuery] int user_id)
        {
            using (var dbContext = new MyProjectDbContext())
            {
                //conferindo se o usuário já existe na base de dados
                var user_search = dbContext.Users.FirstOrDefault(u => u.Id == user_id);
                if (user_search == null)
                {
                    return Results.NotFound("Usuário não Existe!");
                }
                //conferindo login de usuário
                var User_Id = httpContext.Session.GetInt32("User_Id");
                if (User_Id != user_search.Id)
                {
                    return Results.Ok("Você não está logado como esse usuário!");
                }

                User? user = dbContext.Users.Find(user_id);
                return Results.Ok(user);
            }

        }

        public static IResult DeleteById([FromQuery] int user_id)
        {
            using (var dbContext = new MyProjectDbContext())
            {
                User? user = dbContext.Users.Find(user_id);
                if (user == null)
                {
                    return Results.NotFound(); // Retorna que o usuário não foi encontrado
                }

                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
                return Results.Ok(user);
            }

        }

        public static IResult Logar(HttpContext httpContext, string username, string password)
        {
            using (var dbContext = new MyProjectDbContext())
            {
                var users = dbContext.Users.Where(u => u.nome == username && u.password == password).ToList();
                if (users.Count == 0)
                {
                    return Results.Ok("Login Inválido!");
                }
                else
                {
                    var user = users.First();
                    httpContext.Session.SetInt32("User_Id", user.Id);
                    httpContext.Session.SetString("Username", user.nome);
                    httpContext.Session.SetString("Password", user.password);

                    return Results.Ok(new
                    {
                        UserName = httpContext.Session.GetString("Username"),
                        Password = httpContext.Session.GetString("Password"),
                        User_Id = httpContext.Session.GetInt32("User_Id")
                    });
                }
            }

        }

        public static IResult MyUser(HttpContext httpContext)
        {
            using (var dbContext = new MyProjectDbContext())
            {
                if (httpContext.Session.GetInt32("User_Id") != null)
                {
                    var id = httpContext.Session.GetInt32("User_Id").Value;
                    var user = dbContext.Users.Find(id);
                    return Results.Ok(user);
                }
                else
                {
                    return Results.Ok("Você não está logado.");
                }

            }
        }

        public static IResult NovaRequisicaoMatch(HttpContext httpContext, [FromBody] MatchDTO post_content)
        {
            using (var dbContext = new MyProjectDbContext())
            {
                if (httpContext.Session.GetInt32("User_Id") != null)
                {
                    var user_requirinte = dbContext.Users.Find(httpContext.Session.GetInt32("User_Id"));
                    var user_requisitado = dbContext.Users.Find(post_content.id_user_requisitado);
                    var match = new Match();
                    if(user_requirinte!=null && user_requisitado != null)
                    {
                        match.user_requirinte_id = user_requirinte.Id;
                        match.user_requirido_id= user_requisitado.Id;
                    }
                    else
                    {
                        return Results.Ok("Erro ao localizar os usuários da requisição.");
                    }
                    dbContext.Matches.Add(match);
                    dbContext.SaveChanges();

                    return Results.Ok(match);
                }
                else
                {
                    return Results.Ok("Você não está logado.");
                }

            }
        }


    }
}
