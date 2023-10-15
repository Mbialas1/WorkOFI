import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwIfEmpty } from 'rxjs';
import { User } from './models/user.model';

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
  }
  