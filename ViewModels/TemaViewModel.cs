namespace SocialApp.ViewModels
{
    public class TemaViewModel
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }

        public ICollection<Postagem>? Postagens { get; set; }
    }
}
