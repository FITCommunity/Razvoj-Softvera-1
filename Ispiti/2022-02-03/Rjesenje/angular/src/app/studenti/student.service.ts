import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MojConfig } from '../moj-config';
import { Student, StudentAddEditVM } from './student.model';

@Injectable({
  providedIn: 'root',
})
export class StudentService {
  apiURL = `https://localhost:44326/Student/`;
  constructor(private _http: HttpClient) {}

  getAll() {
    return this._http.get<Student[]>(
      `${this.apiURL}GetAll`,
      MojConfig.http_opcije()
    );
  }

  getById(id: number) {
    return this._http.get<Student>(
      `${this.apiURL}Get/${id}`,
      MojConfig.http_opcije()
    );
  }

  add(student: StudentAddEditVM) {
    return this._http.post(
      `${this.apiURL}Update/0`,
      student,
      MojConfig.http_opcije()
    );
  }

  update(student: StudentAddEditVM, id: number) {
    return this._http.post(
      `${this.apiURL}Update/${id}`,
      student,
      MojConfig.http_opcije()
    );
  }

  delete(id: number) {
    return this._http.delete(
      `${this.apiURL}Delete/${id}`,
      MojConfig.http_opcije()
    );
  }
}
