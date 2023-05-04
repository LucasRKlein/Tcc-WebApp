import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Veiculo } from '@app/models/Veiculo';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

@Injectable()
export class VeiculoService {
  baseURL = 'https://localhost:5001/api/veiculo';

  constructor(private http: HttpClient) {}

  public getVeiculosByAssociadoId(associadoId: number): Observable<Veiculo[]> {
    return this.http.get<Veiculo[]>(`${this.baseURL}/${associadoId}`).pipe(take(1));
  }

  public saveVeiculo(associadoId: number, veiculos: Veiculo[]): Observable<Veiculo[]> {
    return this.http
      .put<Veiculo[]>(`${this.baseURL}/${associadoId}`, veiculos)
      .pipe(take(1));
  }

  public deleteVeiculo(associadoId: number, veiculoId: number): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${associadoId}/${veiculoId}`)
      .pipe(take(1));
  }
}
