using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2017_09_11.EntityModels
{
    public class Nastavnik
    {
        [Key]
        public int NastavnikID { get; set; }
        public string ImePrezime { get; set; }
    }
}
