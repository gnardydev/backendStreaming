using Microsoft.AspNetCore.Mvc;
using WebApiStreaming.Data;
using WebApiStreaming.Models;
using Microsoft.EntityFrameworkCore;


namespace WebApiStreaming.Controllers
{
    [ApiController]
    [Route("api/[controller]")]   
    public class PlaylistController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlaylistController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]  //get all playlists      GET: api/playlist
        public async Task<IActionResult> GetAllPlaylists()
        {
            var playlists = await _context.Playlists.Include(p => p.Usuario).ToListAsync();
            return Ok(playlists);
        }

        [HttpGet("{id}")]  //get playlist por id de usuario        GET: api/playlist/{id}
        public async Task<IActionResult> GetPlaylistById(int id)
        {
            var playlist = await _context.Playlists.Include(p => p.Usuario).FirstOrDefaultAsync(p => p.ID == id);
            if (playlist == null) return NotFound();
            return Ok(playlist);
        }

        [HttpPost]  //cria playlist nova       POST: api/playlist
        public async Task<IActionResult> CreatePlaylist(Playlist playlist)
        {
            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPlaylistById), new { id = playlist.ID }, playlist);
        }

        [HttpPut("{id}")]  //atualiza playtlist    PUT: api/playlist/{id}
        public async Task<IActionResult> UpdatePlaylist(int id, Playlist playlist)
        {
            if (id != playlist.ID) return BadRequest();
            _context.Entry(playlist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null) return NotFound();
            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();
            return NoContent();
        }       


        
    }
}
