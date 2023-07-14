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
    public class CvjetsController : ControllerBase
    {
        private readonly CvjecaraContext _context;

        public CvjetsController(CvjecaraContext context)
        {
            _context = context;
        }

        // GET: api/Cvjets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cvjet>>> GetCvjets()
        {
          if (_context.Cvjets == null)
          {
              return NotFound();
          }
            return await _context.Cvjets.ToListAsync();
        }

        // GET: api/Cvjets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cvjet>> GetCvjet(int id)
        {
          if (_context.Cvjets == null)
          {
              return NotFound();
          }
            var cvjet = await _context.Cvjets.FindAsync(id);

            if (cvjet == null)
            {
                return NotFound();
            }

            return cvjet;
        }

        // PUT: api/Cvjets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCvjet(int id, Cvjet cvjet)
        {
            if (id != cvjet.Id)
            {
                return BadRequest();
            }

            _context.Entry(cvjet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CvjetExists(id))
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

        // POST: api/Cvjets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cvjet>> PostCvjet(Cvjet cvjet)
        {
          if (_context.Cvjets == null)
          {
              return Problem("Entity set 'CvjecaraContext.Cvjets'  is null.");
          }
            _context.Cvjets.Add(cvjet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCvjet", new { id = cvjet.Id }, cvjet);
        }

        // DELETE: api/Cvjets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCvjet(int id)
        {
            if (_context.Cvjets == null)
            {
                return NotFound();
            }
            var cvjet = await _context.Cvjets.FindAsync(id);
            if (cvjet == null)
            {
                return NotFound();
            }

            _context.Cvjets.Remove(cvjet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CvjetExists(int id)
        {
            return (_context.Cvjets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
