import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { VistoriaImagem } from '@app/models/VistoriaImagem';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

@Injectable()
export class VistoriaImagemService {
  baseURL = 'https://localhost:5001/api/vistoriaImagem';

  constructor(private http: HttpClient) {}

  public getVistoriaImagemsByVeiculoId(veiculoId: string): Observable<VistoriaImagem[]> {
    return this.http.get<VistoriaImagem[]>(`${this.baseURL}/${veiculoId}`).pipe(take(1));
  }

  public getById(id: string): Observable<VistoriaImagem> {
    return this.http.get<VistoriaImagem>(`${this.baseURL}/GetVistoriaImagemById/${id}`).pipe(take(1));
  }

  public post(veiculo: VistoriaImagem): Observable<VistoriaImagem> {
    return this.http
      .post<VistoriaImagem>(this.baseURL, veiculo)
      .pipe(take(1));
  }

  public put(veiculo: VistoriaImagem): Observable<VistoriaImagem> {
    debugger
    return this.http
      .put<VistoriaImagem>(`${this.baseURL}/${veiculo.id}`, veiculo)
      .pipe(take(1));
  }

  public edit(veiculo: VistoriaImagem): Observable<VistoriaImagem> {
    debugger
    return this.http
      .post<VistoriaImagem>(`${this.baseURL}/edit/${veiculo.id}`, veiculo)
      .pipe(take(1));
  }

  public deleteVistoriaImagem(id: string): Observable<any> {
    return this.http.delete(`${this.baseURL}/${id}`).pipe(take(1));
  }

  public saveVistoriaImagem(veiculoId: string, veiculos: VistoriaImagem[]): Observable<VistoriaImagem[]> {
    return this.http
      .put<VistoriaImagem[]>(`${this.baseURL}/${veiculoId}`, veiculos)
      .pipe(take(1));
  }

  postUploadImagem(vistoriaImagemId: string, file: File): Observable<VistoriaImagem> {
    const fileToUpload = file[0] as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);

    return this.http.post<VistoriaImagem>(`${this.baseURL}/upload-image/${vistoriaImagemId}`, formData).pipe(take(1));
  }
}
