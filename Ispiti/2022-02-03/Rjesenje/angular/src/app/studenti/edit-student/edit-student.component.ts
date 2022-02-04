import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MojConfig } from '../../moj-config';
import { Student, StudentAddEditVM } from '../student.model';

declare function porukaSuccess(s: string): any;

@Component({
  selector: 'app-edit-student',
  templateUrl: './edit-student.component.html',
  styleUrls: ['./edit-student.component.css'],
})
export class EditStudentComponent implements OnInit {
  @Input() student: Student;
  form: FormGroup;
  @Output() edit = new EventEmitter<{
    student: StudentAddEditVM;
    id: number;
  }>();
  @Output() closeModal = new EventEmitter<void>();
  opstine: { id: number; opis: string }[];
  constructor(private _fb: FormBuilder, private _http: HttpClient) {}

  ngOnInit(): void {
    this.form = this._fb.group({
      ime: '',
      prezime: '',
      opstina_rodjenja_id: 0,
    });

    if (this.student != null) {
      this.form.get('ime').patchValue(this.student.ime);
      this.form.get('prezime').patchValue(this.student.prezime);
      this.form
        .get('opstina_rodjenja_id')
        .patchValue(this.student.opstina_rodjenja_id);
    }

    this._http
      .get<{ id: number; opis: string }[]>(
        'https://localhost:44326/Opstina/GetByAll'
      )
      .subscribe((x: { id: number; opis: string }[]) => {
        this.opstine = x;
      });
  }

  onSubmit() {
    const x = { student: this.form.value, id: this.student.id };
    this.edit.emit(x);
  }

  close() {
    this.closeModal.emit();
  }
}
