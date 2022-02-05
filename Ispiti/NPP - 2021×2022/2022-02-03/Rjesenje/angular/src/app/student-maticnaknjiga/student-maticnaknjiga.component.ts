import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MaticnaKnjigaGetVM, UpisUZimskiVM } from './maticna.model';
import { MaticnaService } from './maticna.service';

declare function porukaSuccess(a: string): any;
declare function porukaError(a: string): any;

@Component({
  selector: 'app-student-maticnaknjiga',
  templateUrl: './student-maticnaknjiga.component.html',
  styleUrls: ['./student-maticnaknjiga.component.css'],
})
export class StudentMaticnaknjigaComponent implements OnInit {
  student: MaticnaKnjigaGetVM;
  showUpis = false;

  constructor(
    private _maticna: MaticnaService,
    private _route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.LoadData();
  }

  LoadData() {
    this._maticna
      .GetByStudent(this._route.snapshot.params['id'])
      .subscribe((x: MaticnaKnjigaGetVM) => {
        this.student = x;
      });
  }

  upis() {
    this.showUpis = true;
  }

  upisiZimski(upis: UpisUZimskiVM) {
    upis.studentId = this.student.id;
    this._maticna.UpisiZimski(upis).subscribe(
      () => {
        porukaSuccess('Uspjesno upisan zimski!');
        this.showUpis = false;
        this.LoadData();
      },
      (err: { error: string }) => porukaError(err.error)
    );
  }

  ovjeriZimski(id: number) {
    // this._maticna.OvjerZimski(id).subscribe(
    //   (x) => {
    //     porukaSuccess('ovjerennn');
    //     this.LoadData();
    //   },
    //   (err: { error: string }) => porukaError(err.error)
    // );
    this._maticna.OvjerZimski(id).subscribe(
      () => {
        porukaSuccess('ovjeren!');
        this.LoadData();
      },
      (err) => porukaError(err.error)
    );
  }
}
