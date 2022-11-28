import { Component, Input, OnInit } from '@angular/core';
import { MojConfig } from '../../moj-config';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";

declare function porukaSuccess(s: string): any;

@Component({
  selector: 'app-student-edit',
  templateUrl: './student-edit.component.html',
  styleUrls: ['./student-edit.component.css'],
})
export class StudentEditComponent implements OnInit {
  opstine: any = [];
  @Input() urediStudent:any;
  constructor(private httpClient: HttpClient, private router: ActivatedRoute) {}
  ngOnInit(): void {
    this.loadOpstine();
  }

  loadOpstine(){
    return this.httpClient.get(MojConfig.adresa_servera + "/Opstina/GetByAll", MojConfig.http_opcije())
      .subscribe((res:any) => {
        this.opstine = res;
      })
  }

  spasiPromjene(){
    this.httpClient.post(MojConfig.adresa_servera + "/Student/Update/" + this.urediStudent.id, this.urediStudent, MojConfig.http_opcije())
      .subscribe((res:any) => {
        porukaSuccess(`Izmjene uspjesno spasene!`);
        this.urediStudent.prikazi=false;
      })
  }
}
