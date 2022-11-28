import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";
import {MojConfig} from "../moj-config";

declare function porukaSuccess(a: string): any;
declare function porukaError(a: string): any;

@Component({
  selector: 'app-student-maticnaknjiga',
  templateUrl: './student-maticnaknjiga.component.html',
  styleUrls: ['./student-maticnaknjiga.component.css'],
})
export class StudentMaticnaknjigaComponent implements OnInit {
  maticnaKnjiga: any;
  id: number;
  sub: any;
  godine: any;
  semestar: any = null;

  constructor(private httpClient: HttpClient, private router: ActivatedRoute) {}

  ngOnInit() {
    this.loadGodine();
    this.sub = this.router.params.subscribe((res:any) => {
      this.id = +res["id"];
      this.loadMaticna();
    })
  }

  loadMaticna(){
    this.httpClient.get(MojConfig.adresa_servera + "/MaticnaKnjiga/GetMaticna?id=" + this.id, MojConfig.http_opcije())
      .subscribe((res:any) => {
        this.maticnaKnjiga = res;
      })
  }

  loadGodine(){
    this.httpClient.get(MojConfig.adresa_servera + "/MaticnaKnjiga/GetGodine", MojConfig.http_opcije())
      .subscribe((res:any) => {
        this.godine = res;
      })
  }

  zimskiSemestar(){
    this.semestar = {
      id: this.id,
      datum: new Date(),
      godinaStudija: 1,
      akGodina: 1,
      cijenaSkolarine: 0,
      obnova: false
    }
  }

  upisiZimski(){
    this.httpClient.post(MojConfig.adresa_servera + "/MaticnaKnjiga/UpisiZimski/" + this.id, this.semestar, MojConfig.http_opcije())
      .subscribe((res:any) => {
        porukaSuccess(`Uspjesno upisan zimski semestar`);
        this.semestar=null;
      })
  }

  ovjeriZimski(id: any) {
    this.httpClient.post(MojConfig.adresa_servera + "/MaticnaKnjiga/OvjeriZimski/" + id, MojConfig.http_opcije())
      .subscribe((res:any) => {
        porukaSuccess(`Uspjesno ovjeren zimski semestar`);
      })
  }
}
