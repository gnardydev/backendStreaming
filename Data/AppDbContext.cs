using Microsoft.EntityFrameworkCore;
using WebApiStreaming.Models;

namespace WebApiStreaming.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Conteudo> Conteudos { get; set; }
        public DbSet<Criador> Criadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento entre Usuario e Playlist (1 para n)
            modelBuilder.Entity<Playlist>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Playlists)
                .HasForeignKey(p => p.UsuarioID)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento entre Criador e Conteudo (1 para n)
            modelBuilder.Entity<Conteudo>()
                .HasOne(c => c.Criador)
                .WithMany(cr => cr.Conteudos)
                .HasForeignKey(c => c.CriadorID)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento entre Playlist e Conteudo (n para n)
            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.Conteudos)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

    }
}
