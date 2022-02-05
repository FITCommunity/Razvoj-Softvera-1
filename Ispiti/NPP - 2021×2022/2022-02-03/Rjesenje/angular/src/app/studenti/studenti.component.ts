import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MojConfig } from '../moj-config';
import { Router } from '@angular/router';
import { StudentService } from './student.service';
import { Student, StudentAddEditVM } from './student.model';
declare function porukaSuccess(a: string): any;
declare function porukaError(a: string): any;

@Component({
  selector: 'app-studenti',
  templateUrl: './studenti.component.html',
  styleUrls: ['./studenti.component.css'],
})
export class StudentiComponent implements OnInit {
  title: string = 'angularFIT2';
  studenti: Student[];

  showModal = false;
  editStudent: Student;

  filter = '';

  constructor(private studentService: StudentService) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.studentService.getAll().subscribe((x: Student[]) => {
      this.studenti = x;
    });
  }

  delete(id: number) {
    this.studentService.delete(id).subscribe(() => {
      this.loadData();
      porukaSuccess('Successfully deleted!');
    });
  }

  newStudent() {
    this.showModal = true;
  }
  addStudent(student: StudentAddEditVM) {
    this.studentService.add(student).subscribe(() => {
      this.showModal = false;
      this.loadData();
      porukaSuccess('Successfully added!');
    });
  }

  edit(s: Student) {
    this.editStudent = s;
  }

  onEditStudent(s: { student: StudentAddEditVM; id: number }) {
    this.studentService.update(s.student, s.id).subscribe(() => {
      porukaSuccess('Edited successfully!');
      this.editStudent = null;
      this.loadData();
    });
  }

  filterStudents() {
    if (this.studenti == null) return [];
    return this.studenti.filter(
      (x) =>
        `${x.ime} ${x.prezime}`
          .toLowerCase()
          .startsWith(this.filter.toLowerCase()) ||
        `${x.prezime} ${x.ime}`
          .toLowerCase()
          .startsWith(this.filter.toLowerCase())
    );
  }
}
