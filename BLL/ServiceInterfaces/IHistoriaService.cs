using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOModels.RequestsDTO;
using BLL.DTOModels.ResponseDTO;

namespace BLL.ServiceInterfaces
{
    public interface IHistoriaService
    {
        Task<(List<HistoriaResponseDTO> historie, int totalCount)> GetAllHistoriaAsync(int pageNumber, int pageSize);

        Task<HistoriaResponseDTO> GetHistoriaByIdAsync(int id);

        Task<HistoriaResponseDTO> CreateHistoriaAsync(HistoriaRequestDTO historiaRequestDTO);

        Task<HistoriaResponseDTO> UpdateHistoriaAsync(int id, HistoriaRequestDTO historiaRequestDTO);

        Task<bool> DeleteHistoriaAsync(int id);
    }
}
