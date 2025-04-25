using BLL.DTOModels.RequestsDTO;
using BLL.DTOModels.ResponseDTO;
using BLL.ServiceInterfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL_EF.Services
{
    public class HistoriaService : IHistoriaService
    {
        private readonly PUKolContext _context;

        public HistoriaService(PUKolContext context)
        {
            _context = context;
        }

        public async Task<(List<HistoriaResponseDTO> historie, int totalCount)> GetAllHistoriaAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Historie.CountAsync();

            var historie = await _context.Historie
                                          .Skip((pageNumber - 1) * pageSize)  
                                          .Take(pageSize) 
                                          .ToListAsync();

            var historieDTO = historie.Select(h => new HistoriaResponseDTO
            {
                ID = h.ID,
                Imie = h.Imie,
                Nazwisko = h.Nazwisko,
                GrupaID = h.GrupaID,
                TypAkcji = h.TypAkcji,
                Data = h.Data
            }).ToList();

            return (historieDTO, totalCount);
        }

        public async Task<HistoriaResponseDTO> GetHistoriaByIdAsync(int id)
        {
            var historia = await _context.Historie
                                          .FirstOrDefaultAsync(h => h.ID == id);

            if (historia == null)
            {
                return null; 
            }

            return new HistoriaResponseDTO
            {
                ID = historia.ID,
                Imie = historia.Imie,
                Nazwisko = historia.Nazwisko,
                GrupaID = historia.GrupaID,
                TypAkcji = historia.TypAkcji,
                Data = historia.Data
            };
        }

        public async Task<HistoriaResponseDTO> CreateHistoriaAsync(HistoriaRequestDTO historiaRequestDTO)
        {
            var historia = new Historia
            {
                Imie = historiaRequestDTO.Imie,
                Nazwisko = historiaRequestDTO.Nazwisko,
                GrupaID = historiaRequestDTO.GrupaID,
                TypAkcji = historiaRequestDTO.TypAkcji,
                Data = historiaRequestDTO.Data
            };

            _context.Historie.Add(historia);
            await _context.SaveChangesAsync();

            return new HistoriaResponseDTO
            {
                ID = historia.ID,
                Imie = historia.Imie,
                Nazwisko = historia.Nazwisko,
                GrupaID = historia.GrupaID,
                TypAkcji = historia.TypAkcji,
                Data = historia.Data
            };
        }

        public async Task<HistoriaResponseDTO> UpdateHistoriaAsync(int id, HistoriaRequestDTO historiaRequestDTO)
        {
            var historia = await _context.Historie.FirstOrDefaultAsync(h => h.ID == id);

            if (historia == null)
            {
                return null;
            }

            historia.Imie = historiaRequestDTO.Imie;
            historia.Nazwisko = historiaRequestDTO.Nazwisko;
            historia.GrupaID = historiaRequestDTO.GrupaID;
            historia.TypAkcji = historiaRequestDTO.TypAkcji;
            historia.Data = historiaRequestDTO.Data;

            _context.Historie.Update(historia);
            await _context.SaveChangesAsync();

            return new HistoriaResponseDTO
            {
                ID = historia.ID,
                Imie = historia.Imie,
                Nazwisko = historia.Nazwisko,
                GrupaID = historia.GrupaID,
                TypAkcji = historia.TypAkcji,
                Data = historia.Data
            };
        }

        public async Task<bool> DeleteHistoriaAsync(int id)
        {
            var historia = await _context.Historie.FirstOrDefaultAsync(h => h.ID == id);

            if (historia == null)
            {
                return false;
            }

            _context.Historie.Remove(historia);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
