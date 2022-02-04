export interface MaticnaKnjigaGetVM {
  id: number;
  ime: string;
  prezime: string;
  broj_indeksa: string;
  upisaneAkGodine: AkademskaGodina[];
  cmbStavkeAkademskeGodine: any[];
}

export interface AkademskaGodina {
  id: number;
  datum1_ZimskiUpis: Date;
  datum2_ZimskiOvjera: Date;
  datum3_LjetniUpis: Date;
  datum4_LjetniOvjera: Date;
  godinaStudija: number;
  obnovaGodine: boolean;
  cijenaSkolarine: number;
  evidentiraoKorisnik: any;
  akademskaGodina: any;
}

export interface UpisUZimskiVM {
  datum: Date;
  studentId: number;
  godinaStudija: number;
  akademskaGodinaId: number;
  cijenaSkolarine: number;
  obnovaGodine: boolean;
}
