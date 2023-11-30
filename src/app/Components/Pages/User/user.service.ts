import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

interface LoginResponse {
  token: string;
  // Add other properties if needed, e.g., UserID: number, username: string, etc.
}

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl = 'https://localhost:7263/api/';
  private jwtHelper: JwtHelperService = new JwtHelperService();

  constructor(private httpClient: HttpClient) {}

  // POST request for new user
  addUser(newUser: UserRegister): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.httpClient.post(this.baseUrl + 'User', newUser, {
      headers: headers,
      responseType: 'json',
    });
  }

  // POST request to log users and extract info from the token.
  userLogin(email: string, password: string): Observable<any> {
    const authData = { email: email, password: password };
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.httpClient
      .post<LoginResponse>(this.baseUrl + 'User/Log', authData, {
        headers: headers,
        responseType: 'json',
      })
      .pipe(
        tap((response) => {
          const token = response.token;
          console.log('JWT Token:', token);

          // Decoding
          const decodedToken = this.jwtHelper.decodeToken(token);
          console.log('Decoded Token:', decodedToken);

          // Accessing token infos
          const userId = decodedToken.nameid;
          const userRole = decodedToken.role;
          console.log('UserID:', userId);
          console.log('UserRole:', userRole);
        }),
        catchError((error) => {
          console.error('Error during login:', error);
          throw error;
        })
      );
  }
}

export class UserRegister {
  constructor(public email: string, public password: string) {}
}
