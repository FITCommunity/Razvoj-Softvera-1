import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MojConfig } from '../moj-config';
import { MaticnaKnjigaGetVM, UpisUZimskiVM } from './maticna.model';

@Injectable({
  providedIn: 'root',
})
export class MaticnaService {
  apiURL = `https://localhost:44326/MaticnaKnjiga/`;
  constructor(private _http: HttpClient) {}

  GetByStudent(id: number) {
    return this._http.get<MaticnaKnjigaGetVM>(
      `${this.apiURL}GetByStudent/${id}`,
      MojConfig.http_opcije()
    );
  }

  UpisiZimski(upis: UpisUZimskiVM) {
    return this._http.post(
      `${this.apiURL}UpisiZimski`,
      upis,
      MojConfig.http_opcije()
    );
  }

  OvjerZimski(id: number) {
    return this._http.post(
      `${this.apiURL}OvjeriZimski/${id}`,
      null,
      MojConfig.http_opcije()
    );
  }
}
