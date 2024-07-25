using exemploapi.MVC.DTO;

namespace exemploapi.MVC.model
{
    public class Match
    {
        public int Id { get; set; }

        //essa lista sempre deve ter APENAS DUAS ENTIDADES. sendo o index 0 o requirinte e o index 1 o alvo da requisição

        public int? user_requirinte_id { get; set; }
        public int? user_requirido_id { get; set; }
        public DateTime Criacao { get; set; } = DateTime.Now;
        public bool? MatchAceito { get; set; }
        
    }

}
