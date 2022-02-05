export interface StudentAddEditVM {
  ime: string;
  prezime: string;
  opstina_rodjenja_id: number;
}

export interface Drzava {
  id: number;
  naziv: string;
}

export interface OpstinaRodjenja {
  id: number;
  description: string;
  drzava_id: number;
  drzava: Drzava;
}

export interface Student {
  id: number;
  korisnickoIme: string;
  slika_korisnika: string;
  isNastavnik: boolean;
  isStudent: boolean;
  isAdmin: boolean;
  isProdekan: boolean;
  isDekan: boolean;
  isStudentskaSluzba: boolean;
  ime: string;
  prezime: string;
  broj_indeksa: string;
  opstina_rodjenja_id: number;
  opstina_rodjenja: OpstinaRodjenja;
  datum_rodjenja: Date;
  created_time: Date;
}
