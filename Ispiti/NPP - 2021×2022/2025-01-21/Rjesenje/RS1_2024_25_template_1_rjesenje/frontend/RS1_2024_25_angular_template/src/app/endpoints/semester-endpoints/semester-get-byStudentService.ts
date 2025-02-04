import { inject, Injectable } from "@angular/core";
import { StudentGetByIdResponse } from "../student-endpoints/student-get-by-id-endpoint.service";
import { MyConfig } from "../../my-config";
import { HttpClient } from "@angular/common/http";


export interface SemesterReadResponse{
    id:number;
    studentId:number;
    student:StudentGetByIdResponse;
    profesorId:number;
    prfoesorName:string;
    datumUpisa:Date;
    godinaStudija:number;
    akademskaGodinaId:number;
    akademskaGodinaDescription:string;
    cijenaSkolarine:number;
    obnova:boolean;
}

export interface SemesterRequest{
  studentId:number;
  datumUpisa:Date;
  godinaStudija:number;
  akademskaGodinaId:number;
  cijenaSkolarine:number;
  obnova:boolean;
}

@Injectable({
  providedIn: 'root',
})
export class SemesterGetByStudentService{
     private apiUrl = `${MyConfig.api_address}/semesters/getByStudent`;

     constructor(private httpClient: HttpClient) {}

     getSemesters(id:number){
        return this.httpClient.get<SemesterReadResponse[]>(`${this.apiUrl}/${id}`);
     }
     createSemester(request:SemesterRequest){
      console.log("request",request);
      return this.httpClient.post<number>(`${this.apiUrl}/create`,request);
     }
}

