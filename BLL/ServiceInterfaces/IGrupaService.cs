using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOModels.RequestsDTO;
using BLL.DTOModels.ResponseDTO;

namespace BLL.ServiceInterfaces
{
    public interface IGrupaService
    {
        Task<List<GrupaResponseDTO>> GetAllGrupyAsync();

        Task<GrupaResponseDTO> GetGrupaByIdAsync(int id);

        Task<GrupaResponseDTO> CreateGrupaAsync(GrupaRequestDTO grupaRequestDTO);

        Task<GrupaResponseDTO> UpdateGrupaAsync(int id, GrupaRequestDTO grupaRequestDTO);

        Task<bool> DeleteGrupaAsync(int id);
    }
}
