import { Component, OnInit } from '@angular/core';
import { MojConfig } from '../../moj-config';

declare function porukaSuccess(s: string): any;

@Component({
  selector: 'app-student-edit',
  templateUrl: './student-edit.component.html',
  styleUrls: ['./student-edit.component.css'],
})
export class StudentEditComponent implements OnInit {
  constructor() {}
  ngOnInit(): void {}
}
