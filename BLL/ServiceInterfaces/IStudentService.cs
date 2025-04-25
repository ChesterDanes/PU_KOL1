using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOModels.RequestsDTO;
using BLL.DTOModels.ResponseDTO;

namespace BLL.ServiceInterfaces
{
    public interface IStudentService
    {
        Task<List<StudentResponseDTO>> GetAllStudentsAsync();

        Task<StudentResponseDTO> GetStudentByIdAsync(int id);

        Task<StudentResponseDTO> CreateStudentAsync(StudentRequestDTO studentRequestDTO);

        Task<StudentResponseDTO> UpdateStudentAsync(int id, StudentRequestDTO studentRequestDTO);

        Task<bool> DeleteStudentAsync(int id);
    }
}
