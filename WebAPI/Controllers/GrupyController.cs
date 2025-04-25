using BLL.DTOModels.RequestsDTO;
using BLL.DTOModels.ResponseDTO;
using BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PU_KOL1.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupaController : ControllerBase
    {
        private readonly IGrupaService _grupaService;

        public GrupaController(IGrupaService grupaService)
        {
            _grupaService = grupaService;
        }

        // GET: api/grupa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrupaResponseDTO>>> GetAllGrupy()
        {
            var grupy = await _grupaService.GetAllGrupyAsync();
            return Ok(grupy);
        }

        // GET: api/grupa/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GrupaResponseDTO>> GetGrupaById(int id)
        {
            var grupa = await _grupaService.GetGrupaByIdAsync(id);
            if (grupa == null)
            {
                return NotFound();
            }
            return Ok(grupa);
        }

        // POST: api/grupa
        [HttpPost]
        public async Task<ActionResult<GrupaResponseDTO>> CreateGrupa(GrupaRequestDTO grupaRequestDTO)
        {
            var grupa = await _grupaService.CreateGrupaAsync(grupaRequestDTO);
            return CreatedAtAction(nameof(GetGrupaById), new { id = grupa.ID }, grupa);
        }

        // PUT: api/grupa/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrupa(int id, GrupaRequestDTO grupaRequestDTO)
        {
            var grupa = await _grupaService.UpdateGrupaAsync(id, grupaRequestDTO);
            if (grupa == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/grupa/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrupa(int id)
        {
            var deleted = await _grupaService.DeleteGrupaAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
