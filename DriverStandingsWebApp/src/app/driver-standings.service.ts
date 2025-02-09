import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DriverStanding } from './driver-standing.interface';
import { environment } from '../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class DriverStandingsService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getDriverStandings(year: number): Observable<DriverStanding[]> {
    return this.http.get<DriverStanding[]>(`${this.apiUrl}${year}`);
  }
}