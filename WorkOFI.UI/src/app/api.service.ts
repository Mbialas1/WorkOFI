import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwIfEmpty } from 'rxjs';
import { Task } from './models/task.model';
import { StatusTaskDTO } from './models/statusTaskDTO.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiUrl = 'http://localhost:5089';

  constructor(private http: HttpClient) { }

  getTaskById(id : number): Observable<any> {
    return this.http.get<Task>(`${this.apiUrl}/Tasks/getTask/${id}`);
  }

  addTask(task: Task): Observable<any>{
    return this.http.post(`${this.apiUrl}/Tasks/addTask`, task);
  }

  loadDashboardByUserId(idUser: number): Observable<Task[]>{
    return this.http.get<Task[]>(`${this.apiUrl}/Tasks/dashboard/${idUser}`);
  }

  updateTaskStatus(task: StatusTaskDTO): Observable<any> {
    return this.http.put(`${this.apiUrl}/Tasks/task/changeStatus`, task);
}

}