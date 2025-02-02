import { Injectable } from "@angular/core";
import { MyConfig } from "../../my-config";
import { HttpClient } from '@angular/common/http';

export interface AcademicYear{
    id:number,
    startDate:Date,
    endDate:Date,
    description:string
}

@Injectable({
    providedIn:'root'
})
export class AcademicYearService{

    private apiUrl = `${MyConfig.api_address}/academicYearGetAll`;
    
    constructor(private httpClient: HttpClient) { }
      
    getAll(){
        return this.httpClient.get<AcademicYear[]>(`${this.apiUrl}/all`)
    }

    getById(id:number){
        return this.httpClient.get<AcademicYear>(`${this.apiUrl}/${id}`,)
    }
}