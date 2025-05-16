import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';

export interface CdbRequest {
  initialValue: number;
  months: number;
}

export interface CdbResponse {
  grossValue: number;
  netValue: number;
}

@Injectable({ providedIn: 'root' })
export class CdbService {
  private readonly apiUrl = `${environment.apiUrl}/api/cdbcalculator/calculate`;

  constructor(private http: HttpClient) { }

  calculateCdb(request: CdbRequest): Observable<CdbResponse> {
    return this.http.post<CdbResponse>(this.apiUrl, request);
  }
}
