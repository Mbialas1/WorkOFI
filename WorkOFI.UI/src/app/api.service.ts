import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwIfEmpty } from 'rxjs';
import { Task } from './models/task.model';
import { StatusTaskDTO } from './models/statusTaskDTO.model';
import { LogTimeModel } from './models/logTime.model';
import { Log } from './models/log.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiUrl = 'http://localhost:8081';

  constructor(private http: HttpClient) { }

  getTaskById(id : number): Observable<any> {
    return this.http.get<Task>(`${this.apiUrl}/Tasks/getTask/${id}`);
  }

  addTask(task: Task): Observable<any>{
    return this.http.post(`${this.apiUrl}/Tasks/addTask`, task);
  }

  loadDashboardByUserId(idUser: number, page: number, pageSize: number): Observable<Task[]>{
    return this.http.get<Task[]>(`${this.apiUrl}/Tasks/dashboard/${idUser}/${page}/${pageSize}`);
  }

  updateTaskStatus(task: StatusTaskDTO): Observable<any> {
    return this.http.put(`${this.apiUrl}/Tasks/task/changeStatus`, task);
}

  logTimeToTask(logModel : LogTimeModel): Observable<any>{
    return this.http.post(`${this.apiUrl}/Tasks/task/logTimeToTask`, logModel);
  }

  getTimesLog(idTask: number, page : number, pageSize : number) : Observable<Log[]>{
    return this.http.get<Log[]>(`${this.apiUrl}/Tasks/logTime/${idTask}/${page}/${pageSize}`);
  }

}