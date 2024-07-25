using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using example_db.Data;
using ModelExample.Models;


namespace controller_example.Controllers
{
    static class HelloWorldController{
        public static void AdicionarControllersExemplo(this WebApplication app){
            app.MapGet("/helloworld",HelloWorld);
            app.MapPost("/create/example",ExampleCreate);

        }

        public static string HelloWorld(){
            return "HelloWorld!";
        }

        static IResult ExampleCreate([FromBody] Example post)
        {
            using (var dbContext = new MyProjectDbContext())
            {
                dbContext.Examples.Add(post); 
                dbContext.SaveChanges(); 
                return Results.Ok(post); 
            }
        }




        


    }
    


}

