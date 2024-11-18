namespace WebApiStreaming.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public List<Playlist> Playlists { get; set; } = new List<Playlist>();

    }
}
