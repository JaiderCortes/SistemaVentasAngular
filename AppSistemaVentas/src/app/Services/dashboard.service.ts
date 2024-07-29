import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ResponseAPI } from '../Interfaces/response-api';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  private urlAPI: string = environment.endpoint + 'Dashboard/';
  constructor(private http: HttpClient) {}

  Resumen(): Observable<ResponseAPI> {
    return this.http.get<ResponseAPI>(`${this.urlAPI}Resumen`);
  }
}
