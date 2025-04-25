using BLL.DTOModels.RequestsDTO;
using BLL.DTOModels.ResponseDTO;
using BLL.ServiceInterfaces;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            var historieDTO = new List<HistoriaResponseDTO>();
            int totalCount = 0;

            var connection = _context.Database.GetDbConnection();

            try
            {
                await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "GetHistoriePaged";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var paramPageNumber = new SqlParameter("@PageNumber", System.Data.SqlDbType.Int) { Value = pageNumber };
                var paramPageSize = new SqlParameter("@PageSize", System.Data.SqlDbType.Int) { Value = pageSize };

                command.Parameters.Add(paramPageNumber);
                command.Parameters.Add(paramPageSize);

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    historieDTO.Add(new HistoriaResponseDTO
                    {
                        ID = reader.GetInt32(0),
                        Imie = reader.GetString(1),
                        Nazwisko = reader.GetString(2),
                        GrupaID = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                        TypAkcji = reader.GetString(4),
                        Data = reader.GetDateTime(5)
                    });
                }

                if (await reader.NextResultAsync() && await reader.ReadAsync())
                {
                    totalCount = reader.GetInt32(0);
                }
            }
            finally
            {
                await connection.CloseAsync();
            }

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
