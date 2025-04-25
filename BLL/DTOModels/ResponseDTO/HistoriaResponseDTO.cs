using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels.ResponseDTO
{
    public class HistoriaResponseDTO
    {
        public int ID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int? GrupaID { get; set; }
        public string TypAkcji { get; set; }
        public DateTime Data { get; set; }
    }
}
