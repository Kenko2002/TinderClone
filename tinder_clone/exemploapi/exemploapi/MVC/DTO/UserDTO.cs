using exemploapi.MVC.model;
using System.ComponentModel.DataAnnotations.Schema;

namespace exemploapi.MVC.DTO
{
    public class UserDTO
    {
        //algumas propriedades foram removidas pq o usuário não deve ter permissão de editar elas
        public string? nome { get; set; }

        public string? password { get; set; }
        public DateTime? dataNascimento { get; set; }

        public List<Atracao>? GenerosPelosQuaisSeAtrai { get; set; }

        public string? descricaoPessoal { get; set; }
        public Localizacao? localizacao { get; set; } = new Localizacao();

        public Genero? genero { get; set; }

    }
}
