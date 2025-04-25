using BLL.DTOModels.RequestsDTO;
using BLL.DTOModels.ResponseDTO;
using BLL.ServiceInterfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL_EF.Services
{
    public class GrupaService : IGrupaService
    {
        private readonly PUKolContext _context;

        public GrupaService(PUKolContext context)
        {
            _context = context;
        }

        public async Task<List<GrupaResponseDTO>> GetAllGrupyAsync()
        {
            var grupy = await _context.Grupy.ToListAsync();

            return grupy.Select(g => new GrupaResponseDTO
            {
                ID = g.ID,
                Nazwa = g.Nazwa
            }).ToList();
        }

        public async Task<GrupaResponseDTO> GetGrupaByIdAsync(int id)
        {
            var grupa = await _context.Grupy.FirstOrDefaultAsync(g => g.ID == id);

            if (grupa == null)
                return null;

            return new GrupaResponseDTO
            {
                ID = grupa.ID,
                Nazwa = grupa.Nazwa
            };
        }

        public async Task<GrupaResponseDTO> CreateGrupaAsync(GrupaRequestDTO grupaRequestDTO)
        {
            var grupa = new Grupa
            {
                Nazwa = grupaRequestDTO.Nazwa
            };

            _context.Grupy.Add(grupa);
            await _context.SaveChangesAsync();

            return new GrupaResponseDTO
            {
                ID = grupa.ID,
                Nazwa = grupa.Nazwa
            };
        }

        public async Task<GrupaResponseDTO> UpdateGrupaAsync(int id, GrupaRequestDTO grupaRequestDTO)
        {
            var grupa = await _context.Grupy.FirstOrDefaultAsync(g => g.ID == id);

            if (grupa == null)
                return null;

            grupa.Nazwa = grupaRequestDTO.Nazwa;

            _context.Grupy.Update(grupa);
            await _context.SaveChangesAsync();

            return new GrupaResponseDTO
            {
                ID = grupa.ID,
                Nazwa = grupa.Nazwa
            };
        }

        public async Task<bool> DeleteGrupaAsync(int id)
        {
            var grupa = await _context.Grupy.FirstOrDefaultAsync(g => g.ID == id);

            if (grupa == null)
                return false;

            _context.Grupy.Remove(grupa);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
