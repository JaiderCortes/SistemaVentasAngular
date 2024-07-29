import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ResponseAPI } from '../Interfaces/response-api';
import { Productos } from '../Interfaces/productos';
@Injectable({
  providedIn: 'root',
})
export class ProductosService {
  private urlAPI: string = environment.endpoint + 'Productos/';
  constructor(private http: HttpClient) {}

  Lista(): Observable<ResponseAPI> {
    return this.http.get<ResponseAPI>(`${this.urlAPI}Lista`);
  }

  Guardar(request: Productos): Observable<ResponseAPI> {
    return this.http.post<ResponseAPI>(`${this.urlAPI}Guardar`, request);
  }

  Editar(request: Productos): Observable<ResponseAPI> {
    return this.http.put<ResponseAPI>(`${this.urlAPI}Editar`, request);
  }

  Eliminar(id: number): Observable<ResponseAPI> {
    return this.http.delete<ResponseAPI>(`${this.urlAPI}Eliminar/${id}`);
  }
}
