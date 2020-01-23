using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2019_06_25.EntityModels
{
    public class OdrzaniCas
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        [ForeignKey(nameof(AngazovaniId))]
        public Angazovan Angazovani { get; set; }
        public int AngazovaniId { get; set; }


    }
}