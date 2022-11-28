import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MojConfig } from '../moj-config';
import { Router } from '@angular/router';
declare function porukaSuccess(a: string): any;
declare function porukaError(a: string): any;

@Component({
  selector: 'app-studenti',
  templateUrl: './studenti.component.html',
  styleUrls: ['./studenti.component.css'],
})
export class StudentiComponent implements OnInit {
  title: string = 'angularFIT2';
  filter_imeprezime: string = '';
  filter_opstina: string = '';
  check_imeprezime: boolean;
  check_opstina: boolean;
  studentPodaci: any;
  odabraniStudent: any = null;

  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

  testirajWebApi(): void {
    this.httpKlijent
      .get(
        MojConfig.adresa_servera + '/Student/GetAll',
        MojConfig.http_opcije()
      )
      .subscribe((x: any) => {
        this.studentPodaci = x;
      });
  }

  ngOnInit(): void {
    this.testirajWebApi();
  }

  filter() {
    if (this.check_imeprezime) {
      return this.studentPodaci?.filter((student: any) => `${student.ime} ${student.prezime} || ${student.prezime} ${student.ime}`.toLowerCase().includes(this.filter_imeprezime.toLowerCase()));
    }
    if (this.check_opstina) {
      return this.studentPodaci?.filter((student: any) => `${student.opstina_rodjenja.description}`.toLowerCase().includes(this.filter_opstina.toLowerCase()));
    }
    return this.studentPodaci = null ? [] : this.studentPodaci;
  }

  obrisiStudent(student: any) {
    this.httpKlijent.post(MojConfig.adresa_servera + "/Student/Delete/" + student.id, null, MojConfig.http_opcije())
      .subscribe((res: any) => {
        let index = this.studentPodaci.indexOf(student);
        if (index > -1) {
          this.studentPodaci.splice(student, 1);
          this.testirajWebApi();
        }
      })
    porukaSuccess(`Student uspješno obrisan`)
  }

  dodajStudent(){
    this.odabraniStudent = {
      prikazi: true,
      id: 0,
      ime: this.filter_imeprezime.slice(0,1).toUpperCase() + this.filter_imeprezime.slice(1,).toLowerCase(),
      prezime: "",
      datum_rodjenja: new Date(),
      broj_indeksa: "",
      opstina_rodjenja_id: 2,
      name: "Dodaj student"
    }
  }

  urediStudent(student: any){
    this.odabraniStudent = student;
    this.odabraniStudent.name = "Edit student";
    this.odabraniStudent.prikazi = true;
  }

  maticnaKnjiga(student:any){
    this.router.navigate(["student-maticnaknjiga", student.id]);
  }
}
