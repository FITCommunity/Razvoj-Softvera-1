import { Component, OnInit } from '@angular/core';
import { SemesterGetByStudentService,SemesterReadResponse } from '../../../../endpoints/semester-endpoints/semester-get-byStudentService';
import {ActivatedRoute, Router} from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { StudentGetByIdEndpointService, StudentGetByIdResponse } from '../../../../endpoints/student-endpoints/student-get-by-id-endpoint.service';

@Component({
  selector: 'app-student-semesters',
  standalone: false,
  
  templateUrl: './student-semesters.component.html',
  styleUrl: './student-semesters.component.css'
})
export class StudentSemestersComponent implements OnInit{

  studentId:number;
  student:StudentGetByIdResponse | null = null;
  semesters:SemesterReadResponse[]=[];
  displayedColumns: string[] = ['id', 'akademskaGodinaDescription', 'godinaStudija','obnova','datumUpisa','profesorName'];
  dataSource: MatTableDataSource<SemesterReadResponse> = new MatTableDataSource<SemesterReadResponse>();
  constructor(private semesterService:SemesterGetByStudentService,private route: ActivatedRoute,
    public router: Router,private dialog: MatDialog,private studentService:StudentGetByIdEndpointService) {
    this.studentId = Number(this.route.snapshot.paramMap.get('id'));
  }

  ngOnInit(): void {
    this.getStudent(this.studentId);
    this.getSemesters(this.studentId);
  }

  getStudent(id:number){
    this.studentService.handleAsync(id).subscribe(respnse=>{
      this.student=respnse;
    })
  }

  getSemesters(id:number){
    this.semesterService.getSemesters(id).subscribe(response=>{
      this.semesters=response;
      this.dataSource = new MatTableDataSource<SemesterReadResponse>(this.semesters);
    })
  }

  newSemester(id: number): void{
    console.log("clicked new semester for", this.studentId);
    this.router.navigate(['/admin/student/semester/new',id]);
    }

}
