using Microsoft.EntityFrameworkCore;
using ModelExample.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;
using exemploapi.MVC.model;

namespace example_db.Data
{

    public class MyProjectDbContext: DbContext
    {
        public DbSet<Example> Examples { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Match> Matches { get; set; }

        public DbSet<Privilegios> Privilegios { get; set; }

        public DbSet<Genero> Generos { get; set; }

        public DbSet<Localizacao> Localizacoes { get; set; }

        public DbSet<Atracao> Atracoes { get; set; }


        private string localConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=tinder_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        
        //comandos de terminal uteis:
        //Add-Migration 
        //Update-Database

        public MyProjectDbContext(DbContextOptions<MyProjectDbContext> options) : base(options)
        {

        }
        public MyProjectDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (optionsBuilder.IsConfigured)
                //return;
            optionsBuilder.UseSqlServer(localConnectionString);
            Console.WriteLine("SQL Server inicializado com sucesso!");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Incluir o exemplo pai 
            modelBuilder.Entity<Example>().Navigation(a => a.exemplo_pai).AutoInclude();



        }
        

    }
}





