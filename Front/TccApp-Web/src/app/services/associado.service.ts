import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { PaginatedResult } from '@app/models/Pagination';
import { Observable } from 'rxjs';
import { take, map } from 'rxjs/operators';
import { Associado } from '@app/models/Associado';

@Injectable({
  providedIn: 'root',
})
export class AssociadoService {
  baseURL = environment.apiURL + 'api/associado';

  constructor(private http: HttpClient) { }

  public getAssociados(page?: number, itemsPerPage?: number, term?: string): Observable<PaginatedResult<Associado[]>> {
    const paginatedResult: PaginatedResult<Associado[]> = new PaginatedResult<Associado[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (term != null && term != '') params = params.append('term', term);

    return this.http.get<Associado[]>(this.baseURL + '/all', { observe: 'response', params })
      .pipe(
        take(1),
        map((response) => {
          paginatedResult.result = response.body;
          if (response.headers.has('Pagination')) {
            paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return paginatedResult;
        })
      );
  }
  
  public getAssociadoById(id: number): Observable<Associado> {
    return this.http
      .get<Associado>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public post(associado: Associado): Observable<Associado> {
    return this.http
      .post<Associado>(this.baseURL, associado)
      .pipe(take(1));
  }

  public put(associado: Associado): Observable<Associado> {
    return this.http
      .put<Associado>(`${this.baseURL}/${associado.id}`, associado)
      .pipe(take(1));
  }

  public deleteAssociado(id: number): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  postUpload(associadoId: number, file: File): Observable<Associado> {
    const fileToUpload = file[0] as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);

    return this.http
      .post<Associado>(`${this.baseURL}/upload-image/${associadoId}`, formData)
      .pipe(take(1));
  }
}
