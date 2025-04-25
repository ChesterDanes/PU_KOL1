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
    public class HistoriaController : ControllerBase
    {
        private readonly IHistoriaService _historiaService;

        public HistoriaController(IHistoriaService historiaService)
        {
            _historiaService = historiaService;
        }

        // GET: api/historia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoriaResponseDTO>>> GetAllHistoria(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var (historie, totalCount) = await _historiaService.GetAllHistoriaAsync(pageNumber, pageSize);

            if (historie == null || historie.Count == 0)
                return NotFound("Brak danych.");

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Historie = historie
            };

            return Ok(response);
        }

        // GET: api/historia/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoriaResponseDTO>> GetHistoriaById(int id)
        {
            var historia = await _historiaService.GetHistoriaByIdAsync(id);
            if (historia == null)
            {
                return NotFound();
            }
            return Ok(historia);
        }

        // POST: api/historia
        [HttpPost]
        public async Task<ActionResult<HistoriaResponseDTO>> CreateHistoria(HistoriaRequestDTO historiaRequestDTO)
        {
            var historia = await _historiaService.CreateHistoriaAsync(historiaRequestDTO);
            return CreatedAtAction(nameof(GetHistoriaById), new { id = historia.ID }, historia);
        }

        // PUT: api/historia/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHistoria(int id, HistoriaRequestDTO historiaRequestDTO)
        {
            var historia = await _historiaService.UpdateHistoriaAsync(id, historiaRequestDTO);
            if (historia == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/historia/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoria(int id)
        {
            var deleted = await _historiaService.DeleteHistoriaAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
