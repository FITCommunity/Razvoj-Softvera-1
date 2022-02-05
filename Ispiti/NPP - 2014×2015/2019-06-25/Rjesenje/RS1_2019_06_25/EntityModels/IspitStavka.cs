using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_06_25.EntityModels
{
    public class IspitStavka
    {
        public int Id { get; set; }
        public int IspitId { get; set; }
        public Ispit Ispit { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int Ocjena { get; set; }
        public bool IsPristupio { get; set; }
    }

}
