using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gus.Models
{
    public class Area
    {
        public int id { get; set; }
        public string nazwa { get; set; }
        public int id_nadrzedny_element { get; set; }
        public int id_poziom { get; set; }
        public string nazwa_poziom { get; set; }
        public bool czy_zmienne { get; set; }
    }
}
