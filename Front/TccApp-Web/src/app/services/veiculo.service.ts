import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Veiculo } from '@app/models/Veiculo';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

@Injectable()
export class VeiculoService {
  baseURL = 'https://localhost:5001/api/veiculo';

  constructor(private http: HttpClient) {}

  public getVeiculosByAssociadoId(associadoId: string): Observable<Veiculo[]> {
    return this.http.get<Veiculo[]>(`${this.baseURL}/${associadoId}`).pipe(take(1));
  }

  public getById(id: string): Observable<Veiculo> {
    return this.http.get<Veiculo>(`${this.baseURL}/GetVeiculoById/${id}`).pipe(take(1));
  }

  public post(veiculo: Veiculo): Observable<Veiculo> {
    return this.http
      .post<Veiculo>(this.baseURL, veiculo)
      .pipe(take(1));
  }

  public put(veiculo: Veiculo): Observable<Veiculo> {
    debugger
    return this.http
      .put<Veiculo>(`${this.baseURL}/${veiculo.id}`, veiculo)
      .pipe(take(1));
  }

  public edit(veiculo: Veiculo): Observable<Veiculo> {
    debugger
    return this.http
      .post<Veiculo>(`${this.baseURL}/edit/${veiculo.id}`, veiculo)
      .pipe(take(1));
  }

  public deleteVeiculo(id: string): Observable<any> {
    return this.http.delete(`${this.baseURL}/${id}`).pipe(take(1));
  }

  public saveVeiculo(veiculoId: string, veiculos: Veiculo[]): Observable<Veiculo[]> {
    return this.http
      .put<Veiculo[]>(`${this.baseURL}/${veiculoId}`, veiculos)
      .pipe(take(1));
  }
}
