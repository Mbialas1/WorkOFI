import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { User } from './models/user.model';
import { LoginComponentModel } from './models/loginComponent.model';
import { RegisterLoginComponentModel } from './models/registerLoginComponents';
import { map, catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
  })
  export class UserService {
    private apiUrl = 'http://localhost:8080';
  
    constructor(private http: HttpClient) {}
    
    getAllUsers(): Observable<User[]> {
      return this.http.get<User[]>(`${this.apiUrl}/Users/users/allUsers`);
    }

    

searchUsers(query: string): Observable<User[]> {
  return this.http.get<User[]>(`${this.apiUrl}/Users/users/search`, { params: { characters: query } });
}

loginToApplication(login : LoginComponentModel) : Observable<boolean>{
  return this.http.post(`${this.apiUrl}'/Users/auth/login`, login).pipe(
    map(() => true),
    catchError(error => {
      if (error.status !== 200) {
        return of(false);
      }
      throw error;
    })
  );
}

registerToApplication(newUser : RegisterLoginComponentModel) : Observable<boolean>{
  return this.http.post(`${this.apiUrl}/Users/auth/register`, newUser).pipe(
    map(() => true),
    catchError(error => {
      if (error.status !== 200) {
        return of(false);
      }
      throw error;
    })
  );
  }}
