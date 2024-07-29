import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ResponseAPI } from '../Interfaces/response-api';
import { Ventas } from '../Interfaces/ventas';

@Injectable({
  providedIn: 'root',
})
export class VentasService {
  private urlAPI: string = environment.endpoint + 'Ventas/';
  constructor(private http: HttpClient) {}

  Registrar(request: Ventas): Observable<ResponseAPI> {
    return this.http.post<ResponseAPI>(`${this.urlAPI}Registrar`, request);
  }

  Historial(
    buscarPor: string,
    numVenta: string,
    fechaInicio: string,
    fechaFin: string
  ): Observable<ResponseAPI> {
    return this.http.get<ResponseAPI>(
      `${this.urlAPI}Historial?buscarPor=${buscarPor}&numVenta=${numVenta}&fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`
    );
  }

  Reporte(fechaInicio: string, fechaFin: string): Observable<ResponseAPI> {
    return this.http.get<ResponseAPI>(
      `${this.urlAPI}Reporte?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`
    );
  }
}
