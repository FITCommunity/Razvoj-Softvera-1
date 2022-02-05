namespace RS1_2018_01_23.EntityModels
{
    public class Modalitet
    {
        public int Id { get; set; }
        public string Opis { get; set; }
        public bool IsReferentnaVrijednost { get; set; }
        public LabPretraga LabPretraga { get; set; }
        public int LabPretragaId  { get; set; }
    }
}
