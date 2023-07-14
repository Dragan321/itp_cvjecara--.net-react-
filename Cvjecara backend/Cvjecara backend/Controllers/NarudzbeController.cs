using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cvjecara_backend.DataModels;

namespace Cvjecara_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NarudzbeController : ControllerBase
    {
        private readonly CvjecaraContext _context;

        public NarudzbeController(CvjecaraContext context)
        {
            _context = context;
        }

        // GET: api/Narudzbe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Narudzba>>> GetNarudzbas()
        {
          if (_context.Narudzbas == null)
          {
              return NotFound();
          }
            return await _context.Narudzbas.ToListAsync();
        }

        // GET: api/Narudzbe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Narudzba>> GetNarudzba(int id)
        {
          if (_context.Narudzbas == null)
          {
              return NotFound();
          }
            var narudzba = await _context.Narudzbas.FindAsync(id);

            if (narudzba == null)
            {
                return NotFound();
            }

            return narudzba;
        }

        // PUT: api/Narudzbe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNarudzba(int id, Narudzba narudzba)
        {
            if (id != narudzba.Id)
            {
                return BadRequest();
            }

            _context.Entry(narudzba).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NarudzbaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Narudzbe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Narudzba>> PostNarudzba(Narudzba narudzba)
        {
          if (_context.Narudzbas == null)
          {
              return Problem("Entity set 'CvjecaraContext.Narudzbas'  is null.");
          }
            foreach (var item in narudzba.Sadrzajnarudzbes)
            {
                _context.Cvjets.Find(item.CvjId).Kolicina -= item.Kolicina;
            }
            _context.Narudzbas.Add(narudzba);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNarudzba", new { id = narudzba.Id }, narudzba);
        }

        // DELETE: api/Narudzbe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNarudzba(int id)
        {
            if (_context.Narudzbas == null)
            {
                return NotFound();
            }
            var narudzba = await _context.Narudzbas.FindAsync(id);
            if (narudzba == null)
            {
                return NotFound();
            }

            _context.Narudzbas.Remove(narudzba);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NarudzbaExists(int id)
        {
            return (_context.Narudzbas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
