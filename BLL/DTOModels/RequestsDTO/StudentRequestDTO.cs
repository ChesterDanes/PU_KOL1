using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels.RequestsDTO
{
    public class StudentRequestDTO
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int? GrupaID { get; set; }
    }
}
