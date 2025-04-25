using BLL.DTOModels.RequestsDTO;
using BLL.DTOModels.ResponseDTO;
using BLL.ServiceInterfaces;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL_EF.Services
{
    public class StudentService : IStudentService
    {
        private readonly PUKolContext _context;

        public StudentService(PUKolContext context)
        {
            _context = context;
        }


        public async Task<List<StudentResponseDTO>> GetAllStudentsAsync()
        {
            var students = await _context.Studenci
                                         .Include(s => s.Grupa)
                                         .ToListAsync();

            return students.Select(s => new StudentResponseDTO
            {
                ID = s.ID,
                Imie = s.Imie,
                Nazwisko = s.Nazwisko,
                GrupaID = s.Grupa?.ID
            }).ToList();
        }


        public async Task<StudentResponseDTO> GetStudentByIdAsync(int id)
        {
            var student = await _context.Studenci
                                        .Include(s => s.Grupa) 
                                        .FirstOrDefaultAsync(s => s.ID == id);

            if (student == null)
                return null;

            return new StudentResponseDTO
            {
                ID = student.ID,
                Imie = student.Imie,
                Nazwisko = student.Nazwisko,
                GrupaID = student.Grupa?.ID
            };
        }


        public async Task<StudentResponseDTO> CreateStudentAsync(StudentRequestDTO studentRequestDTO)
        {
            var imieParam = new SqlParameter("@Imie", studentRequestDTO.Imie);
            var nazwiskoParam = new SqlParameter("@Nazwisko", studentRequestDTO.Nazwisko);
            var grupaIdParam = new SqlParameter("@GrupaID", studentRequestDTO.GrupaID ?? (object)DBNull.Value);

            await _context.Database.ExecuteSqlRawAsync("EXEC AddStudent @Imie, @Nazwisko, @GrupaID", imieParam, nazwiskoParam, grupaIdParam);

         
            var student = await _context.Studenci
                .Where(s => s.Imie == studentRequestDTO.Imie && s.Nazwisko == studentRequestDTO.Nazwisko)
                .OrderByDescending(s => s.ID)
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return null;
            }

            return new StudentResponseDTO
            {
                ID = student.ID,
                Imie = student.Imie,
                Nazwisko = student.Nazwisko,
                GrupaID = student.GrupaID
            };
        }

        public async Task<StudentResponseDTO> UpdateStudentAsync(int id, StudentRequestDTO studentRequestDTO)
        {
            var student = await _context.Studenci.FirstOrDefaultAsync(s => s.ID == id);

            if (student == null)
                return null;

            student.Imie = studentRequestDTO.Imie;
            student.Nazwisko = studentRequestDTO.Nazwisko;
            student.GrupaID = studentRequestDTO.GrupaID;

            _context.Studenci.Update(student);
            await _context.SaveChangesAsync();

            return new StudentResponseDTO
            {
                ID = student.ID,
                Imie = student.Imie,
                Nazwisko = student.Nazwisko,
                GrupaID = student.Grupa?.ID
            };
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Studenci.FirstOrDefaultAsync(s => s.ID == id);

            if (student == null)
                return false;

            _context.Studenci.Remove(student);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
