import { Component, OnInit } from '@angular/core';

declare function porukaSuccess(a: string): any;
declare function porukaError(a: string): any;

@Component({
  selector: 'app-student-maticnaknjiga',
  templateUrl: './student-maticnaknjiga.component.html',
  styleUrls: ['./student-maticnaknjiga.component.css'],
})
export class StudentMaticnaknjigaComponent implements OnInit {
  constructor() {}

  ngOnInit() {}

  ovjeriLjetni() {}

  upisLjetni() {}

  ovjeriZimski() {}
}
