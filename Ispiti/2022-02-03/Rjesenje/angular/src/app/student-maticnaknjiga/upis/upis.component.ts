import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { UpisUZimskiVM } from '../maticna.model';

@Component({
  selector: 'app-upis',
  templateUrl: './upis.component.html',
  styleUrls: ['./upis.component.css'],
})
export class UpisComponent implements OnInit {
  @Input() godine: { id: number; opis: string }[];
  @Input() student: string;
  @Output() closeModal = new EventEmitter<void>();
  @Output() result = new EventEmitter<UpisUZimskiVM>();
  form: FormGroup;
  constructor(private _fb: FormBuilder) {}

  ngOnInit(): void {
    this.form = this._fb.group({
      datum: '',
      studentId: 0,
      godinaStudija: 0,
      akademskaGodinaId: 0,
      cijenaSkolarine: 0,
      obnovaGodine: false,
    });
  }

  close() {
    this.closeModal.emit();
  }

  submit() {
    this.result.emit(this.form.value);
  }
}
