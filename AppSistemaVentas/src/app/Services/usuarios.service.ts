import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ResponseAPI } from '../Interfaces/response-api';
import { Login } from '../Interfaces/login';
import { Usuarios } from '../Interfaces/usuarios';

@Injectable({
  providedIn: 'root',
})
export class UsuariosService {
  private urlAPI: string = environment.endpoint + 'Usuarios/';
  constructor(private http: HttpClient) {}

  IniciarSesion(request: Login): Observable<ResponseAPI> {
    return this.http.post<ResponseAPI>(`${this.urlAPI}IniciarSesion`, request);
  }

  Lista(): Observable<ResponseAPI> {
    return this.http.get<ResponseAPI>(`${this.urlAPI}Lista`);
  }

  Guardar(request: Usuarios): Observable<ResponseAPI> {
    return this.http.post<ResponseAPI>(`${this.urlAPI}Guardar`, request);
  }

  Editar(request: Usuarios): Observable<ResponseAPI> {
    return this.http.put<ResponseAPI>(`${this.urlAPI}Editar`, request);
  }

  Eliminar(id: number): Observable<ResponseAPI> {
    return this.http.delete<ResponseAPI>(`${this.urlAPI}Eliminar/${id}`);
  }
}
