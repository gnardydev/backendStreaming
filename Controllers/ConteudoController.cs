using Microsoft.AspNetCore.Mvc;
using WebApiStreaming.Data;
using WebApiStreaming.Models;
using Microsoft.EntityFrameworkCore;



namespace WebApiStreaming.Controllers
{

    [ApiController]
    [Route("api/[controller]")] 
    public class ConteudoController : ControllerBase
    {
        private readonly AppDbContext _context; 

        public ConteudoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]      //Retorna todos os conteúdos    GET: api/conteudo 
        public async Task<IActionResult> GetAllContents()
        {
            var conteudos = await _context.Conteudos.Include(c => c.Criador).ToListAsync();
            return Ok(conteudos);
        }
       
        [HttpGet("{id}")]   //Retorna um conteúdo específico pelo id      GET: api/conteudo/{id} 
        public async Task<IActionResult> GetContentById(int id)
        {
            var conteudo = await _context.Conteudos.Include(c => c.Criador).FirstOrDefaultAsync(c => c.ID == id);
            if (conteudo == null) return NotFound(); 
            return Ok(conteudo); 
        }

        [HttpPost]  //Cria um novo conteúdo.           POST: api/conteudo 
        public async Task<IActionResult> CreateContent(Conteudo conteudo)
        {
            _context.Conteudos.Add(conteudo); 
            await _context.SaveChangesAsync();                                               
            return CreatedAtAction(nameof(GetContentById), new { id = conteudo.ID }, conteudo);
        }

        
        [HttpPut("{id}")]   //Atualiza um conteúdo existent              PUT: api/conteudo/{id}
        public async Task<IActionResult> UpdateContent(int id, Conteudo conteudo)
        {
            if (id != conteudo.ID) return BadRequest();
            _context.Entry(conteudo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

        }
                      
        [HttpDelete("{id}")] //Exclui um conteúdo           DELETE: api/conteudo/{id}
        public async Task<IActionResult> DeleteContent(int id)
        {
            var conteudo = await _context.Conteudos.FindAsync(id); // Busca o conteúdo pelo ID.
            if (conteudo == null) return NotFound(); // Retorna 404 se não for encontrado.
            _context.Conteudos.Remove(conteudo); // Remove o conteúdo do contexto.
            await _context.SaveChangesAsync(); // Salva as alterações no banco.
            return NoContent(); // Retorna 204 (Sem conteúdo) para indicar sucesso.
        }
    }
    
}
