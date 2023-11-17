import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TimeLog } from './models/time-log.model';

@Injectable({
  providedIn: 'root'
})
export class TimeLogService {
  private apiUrl = 'http://localhost:5000/timelogs'; 

  constructor(private http: HttpClient) {
    console.log('TimeLogService instantiated');
  }

  getTimeLogs(pageIndex: number, pageSize: number): Observable<{ data: TimeLog[]; totalCount: number; }> {
    return this.http.get<{ data: TimeLog[]; totalCount: number; }>(`${this.apiUrl}?pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }

  getTimeLog(id: number): Observable<TimeLog> {
    return this.http.get<TimeLog>(`${this.apiUrl}/${id}`);
  }

  postTimeLog(timeLog: TimeLog): Observable<TimeLog> {
    return this.http.post<TimeLog>(this.apiUrl, timeLog);
  }

  putTimeLog(id: number, timeLog: TimeLog): Observable<TimeLog> {
    return this.http.put<TimeLog>(`${this.apiUrl}/${id}`, timeLog);
  }

  deleteTimeLog(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
