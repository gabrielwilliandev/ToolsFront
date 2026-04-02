import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'https://localhost:7130/api/Auth'; // Ajuste para o endpoint real do backend

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, { email, password });
  }

  register(nome: string, email: string, password: string, confirmPass: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, { nome, email, password, confirmPass });
  }

  setToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  logout() {
    localStorage.removeItem('token');
    window.location.href = '/login';
  }
  isAuthenticated(): boolean {
    return !!this.getToken();
  }
}
