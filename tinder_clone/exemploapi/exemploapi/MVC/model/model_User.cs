using exemploapi.MVC.DTO;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace exemploapi.MVC.model
{
    public class User
    {
        public int Id { get; set; }
        public string? nome { get; set; }

        public Privilegios? privilegios { get; set; } = new Privilegios();
        public string? password { get; set; }
        public DateTime? dataNascimento { get; set; }

        public List<Atracao>? GenerosPelosQuaisSeAtrai { get; set; }

        public string? descricaoPessoal { get; set; }
        public Localizacao? localizacao { get; set; } = new Localizacao();

        public Genero? genero { get; set; }

        public List<Match>? matches { get; set; } = new List<Match>();



        public void transferirInformacao_DTO(UserDTO dto)
        {
            nome = dto.nome;
            password = dto.password;
            descricaoPessoal = dto.descricaoPessoal;
            localizacao = dto.localizacao;
            genero = dto.genero;
            dataNascimento = dto.dataNascimento;
            GenerosPelosQuaisSeAtrai = dto.GenerosPelosQuaisSeAtrai;
            //matches,privilegios, e id são gerenciados pelo back end e não por formulários.
        }


    }


}
