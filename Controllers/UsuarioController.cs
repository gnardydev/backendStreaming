using Microsoft.AspNetCore.Mvc;
using WebApiStreaming.Data;
using WebApiStreaming.Models;
using Microsoft.EntityFrameworkCore;


namespace WebApiStreaming.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]    //get all users            GET: api/usuario
        public async Task<IActionResult> GetAllUsers()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]  //get user pelo id dele    GET: api/usuario/{id}
        public async Task<IActionResult> GetUsersById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]  //Cria um usuario novo        POST: api/usuario
        public async Task<ActionResult> CreateUser(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsersById), new { id = usuario.ID }, usuario);

        }

        [HttpPut("{id}")]     //Atualiza um usuario existente     PUT: api/usuario/{id}
        public async Task<ActionResult> UpdateUser(int id, Usuario usuario)
        {
            if (id != usuario.ID) return BadRequest();
            _context.Entry(usuario).State = EntityState.Modified; // marcar objeto como alterado
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]  //Deleta um usuario pelo id     DELETE: api/usuario/{id}
        public async Task<IActionResult> DeleteUser(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();

        }


    }
}