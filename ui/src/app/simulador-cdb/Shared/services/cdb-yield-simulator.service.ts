import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { SimulateRequest } from '../SimulateRequest';
import { SimulateResponse } from '../SimulateResponse';
import { Observable } from 'rxjs';
import { Result } from '../result.model';

@Injectable({
  providedIn: 'root',
})
export class CdbYieldSimulatorService {
  private readonly apiUrl = environment.apiUrl.concat('/investment/simulate');
  constructor(private http: HttpClient) {}

  simulate(request: SimulateRequest): Observable<Result<SimulateResponse>> {
    return this.http.post<Result<SimulateResponse>>(this.apiUrl, request);
  }
}
