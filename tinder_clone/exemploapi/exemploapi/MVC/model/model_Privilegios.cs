namespace exemploapi.MVC.model
{
    public class Privilegios
    {
        public int Id { get; set; }
        public bool isAdmin { get; set; }
        public bool isPremium { get; set; }
        public bool isFree { get; set; }


        public Privilegios()
        {
            this.isAdmin = false;
            this.isPremium = false;
            this.isFree = true;
        }



    }
}
