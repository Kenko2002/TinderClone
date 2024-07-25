using System;

namespace ModelExample.Models
{
    public class Example
    {
        public int Id { get; set; }
        public string? Nome { get; set; }

        public Example? exemplo_pai { get; set; }


        public Example(string nome)
        {
            this.Id = 0;
            this.Nome = nome;
        }

        public Example(string nome, Example exemplo_pai)
        {
            this.Id = 0;
            this.Nome = nome;
            this.exemplo_pai = exemplo_pai;
        }

        public Example()
        {
            this.Nome = null;
        }


    }

    
}