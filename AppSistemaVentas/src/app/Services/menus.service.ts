import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ResponseAPI } from '../Interfaces/response-api';

@Injectable({
  providedIn: 'root',
})
export class MenusService {
  private urlAPI: string = environment.endpoint + 'Menus/';
  constructor(private http: HttpClient) {}

  Lista(idUsuario: number): Observable<ResponseAPI> {
    return this.http.get<ResponseAPI>(
      `${this.urlAPI}Lista?idUsuario=${idUsuario}`
    );
  }
}
