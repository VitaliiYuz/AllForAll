import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userUrl = 'http://localhost:5158/api/users';

  constructor(private http: HttpClient) {
  }

  getUserById(id: number): Observable<User> {
    const url = `${this.userUrl}/GetUserByIdAsync/${id}`;
    return this.http.get<User>(url);
  }

}
