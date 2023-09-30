import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from './models/task.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiUrl = 'http://localhost:5089'; // Adres Twojego API

  constructor(private http: HttpClient) { }

  getTaskById(id : number): Observable<any> {
    return this.http.get<Task>(`${this.apiUrl}/Tasks/getTask/${id}`);
  }
}