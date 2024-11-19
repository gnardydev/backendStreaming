using Microsoft.AspNetCore.Mvc;
using WebApiStreaming.Data;
using WebApiStreaming.Models;
using Microsoft.EntityFrameworkCore;


namespace WebApiStreaming.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 

    public class CriadorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CriadorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]   //get todos criadores GET: api/criador
        public async Task<IActionResult> GetAllCreators()
        {
            var criadores = await _context.Criadores.ToListAsync();
            return Ok(criadores);

        }
         
        [HttpGet("{id}")]  //pega criador pelo id  GET: api/criador/{id}
        public async Task<IActionResult> GetCreatorById(int id)
        {
            var criador = await _context.Criadores.FindAsync(id); 
            if (criador == null) return NotFound(); 
            return Ok(criador); 
        }

        [HttpPost] //cria novo criador    POST: api/criador
        public async Task<IActionResult> CreateCreator(Criador criador)
        {
            _context.Criadores.Add(criador); 
            await _context.SaveChangesAsync();                                     
            return CreatedAtAction(nameof(GetCreatorById), new { id = criador.ID }, criador);
        }

        [HttpPut("{id}")]   //atualiza criador pelo id     PUT: api/criador/{id}
        public async Task<IActionResult> UpdateCreator(int id, Criador criador)
        {
            if (id != criador.ID) return BadRequest(); 
            _context.Entry(criador).State = EntityState.Modified; 
            await _context.SaveChangesAsync(); 
            return NoContent(); 
        }

        [HttpDelete("{id}")]  //deleta criador   DELETE: api/criador/{id}
        public async Task<IActionResult> DeleteCreator(int id)
        {
            var criador = await _context.Criadores.FindAsync(id); 
            if (criador == null) return NotFound(); 
            _context.Criadores.Remove(criador); 
            await _context.SaveChangesAsync(); 
            return NoContent(); 
        }
    }
}
