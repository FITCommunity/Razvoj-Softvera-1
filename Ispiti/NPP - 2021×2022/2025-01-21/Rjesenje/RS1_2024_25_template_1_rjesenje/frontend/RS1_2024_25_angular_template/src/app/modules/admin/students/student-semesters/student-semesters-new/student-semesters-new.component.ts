import { Component, OnInit } from '@angular/core';
import { StudentGetByIdEndpointService, StudentGetByIdResponse } from '../../../../../endpoints/student-endpoints/student-get-by-id-endpoint.service';
import { SemesterGetByStudentService,SemesterReadResponse, SemesterRequest } from '../../../../../endpoints/semester-endpoints/semester-get-byStudentService';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AcademicYearService,AcademicYear } from '../../../../../endpoints/academic-year-endpoint.ts/academic-year-get-endpoint';


@Component({
  selector: 'app-student-semesters-new',
  standalone: false,
  
  templateUrl: './student-semesters-new.component.html',
  styleUrl: './student-semesters-new.component.css'
})
export class StudentSemestersNewComponent implements OnInit {

  studentId:number;
  student:StudentGetByIdResponse | null = null;
  semesterForm:FormGroup;
  akademskeGodine:AcademicYear[]=[];
  semesters:SemesterReadResponse[]=[];
  constructor(private semesterService:SemesterGetByStudentService,private route: ActivatedRoute,
  public router: Router,private dialog: MatDialog,private studentService:StudentGetByIdEndpointService,
  private fb: FormBuilder,private academicYearService:AcademicYearService){
      this.studentId=Number(this.route.snapshot.paramMap.get('id'));
      this.semesterForm=this.fb.group({
        datum:[''],
        godinaStudija:[''],
        akGodina:[''],
        cijenaSkolarine:[1800],
        obnova:[false]
      })
    
    }

  ngOnInit(): void {
    this.getStudent(this.studentId);
    this.getSemesters(this.studentId);
    this.getAkademskeGodine();

    this.semesterForm.get('godinaStudija')?.valueChanges.subscribe(value=>{
      let exists=false;
      for(let i=0;i<this.semesters.length;i++){
        if(this.semesters[i].godinaStudija===Number(value))
          {
            exists=true;
            break;
          }
        }
        if(exists){
          this.semesterForm.get('obnova')?.setValue(true);
        }else{
          this.semesterForm.get('obnova')?.setValue(false);

        }
      })
      
      this.semesterForm.get('obnova')?.valueChanges.subscribe((checked: boolean) => {
        this.semesterForm.patchValue({ cijenaSkolarine: checked ? 400 : 1800 });
      });

      this.semesterForm.get('cijenaSkolarine')?.disable();
      this.semesterForm.get('obnova')?.disable();
  }
  
  getStudent(id:number){
    this.studentService.handleAsync(id).subscribe(respnse=>{
      this.student=respnse;
    })
  }
  
     getSemesters(id:number){
        this.semesterService.getSemesters(id).subscribe(response=>{
          this.semesters=response;
          console.log("Semestri",this.semesters);
        });
      }

  getAkademskeGodine(){
    this.academicYearService.getAll().subscribe(response=>{
      this.akademskeGodine=response;
    })
  }

  saveSemester():void{
  
    if(this.semesterForm.invalid) return;

    const semesterData: SemesterRequest = {
      studentId: this.studentId,
      datumUpisa: this.semesterForm.value.datum,
      godinaStudija: Number(this.semesterForm.value.godinaStudija),
      akademskaGodinaId: this.semesterForm.value.akGodina,
      cijenaSkolarine: this.semesterForm.getRawValue().cijenaSkolarine,
     obnova: this.semesterForm.getRawValue().obnova
    };

    console.log("Semester Data: ", semesterData);


    this.semesterService.createSemester(semesterData).subscribe({
      next:()=>{
        this.router.navigate(['/admin/student/semester',this.studentId]);
      },
      error:()=>{
        console.error("Error saving semesters");
      }
    })
  }
}
