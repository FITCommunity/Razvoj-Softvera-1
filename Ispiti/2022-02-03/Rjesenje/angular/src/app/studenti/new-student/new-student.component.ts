import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { StudentAddEditVM } from '../student.model';

@Component({
  selector: 'app-new-student',
  templateUrl: './new-student.component.html',
  styleUrls: ['./new-student.component.css'],
})
export class NewStudentComponent implements OnInit {
  form: FormGroup;
  @Output() result = new EventEmitter<StudentAddEditVM>();
  @Output() closeModal = new EventEmitter<void>();
  opstine: { id: number; opis: string }[];

  constructor(private _fb: FormBuilder, private _http: HttpClient) {}

  ngOnInit(): void {
    this.form = this._fb.group({
      ime: '',
      prezime: '',
      opstina_rodjenja_id: 0,
    });

    this._http
      .get<{ id: number; opis: string }[]>(
        'https://localhost:44326/Opstina/GetByAll'
      )
      .subscribe((x: { id: number; opis: string }[]) => {
        this.opstine = x;
      });
  }

  onSubmit() {
    this.result.emit(this.form.value);
  }

  close() {
    this.closeModal.emit();
  }
}
