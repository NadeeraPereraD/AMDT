import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private authBaseUrl = environment.authBaseUrl;
  private accessTokenKey = 'accessToken';
  private refreshTokenKey = 'refreshToken';
  private jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient, private router: Router) {}

  login(credentials: {
    username: string;
    password: string;
    appName: string;
  }): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.post(`${this.authBaseUrl}/User/Login`, credentials, {
      headers,
    });
  }

  // Logout and remove tokens
  logout() {
    localStorage.removeItem(this.accessTokenKey);
    localStorage.removeItem(this.refreshTokenKey);
    this.router.navigate(['/login']);
  }

  // Check if the user is authenticated
  isAuthenticated(): boolean {
    const token = this.getAccessToken();
    return !!token && !this.jwtHelper.isTokenExpired(token);
  }

  // Get the stored access token
  getAccessToken(): string | null {
    return localStorage.getItem(this.accessTokenKey);
  }

  // Get the stored refresh token
  getRefreshToken(): string | null {
    return localStorage.getItem(this.refreshTokenKey);
  }

  // Set both access and refresh tokens
  setTokens(accessToken: string, refreshToken: string): void {
    localStorage.setItem(this.accessTokenKey, accessToken);
    localStorage.setItem(this.refreshTokenKey, refreshToken);
  }

  // Refresh the access token
  refreshToken() {
    const refreshToken = this.getRefreshToken();
    if (!refreshToken) {
      this.logout();
      return;
    }

    return this.http.post('/api/auth/refresh', { refreshToken }).subscribe(
      (res: any) => {
        this.setTokens(res.accessToken, res.refreshToken);
      },
      (error) => {
        this.logout();
      }
    );
  }

  // Get user info from the token
  getUserInfo() {
    const token = this.getAccessToken();
    if (token) {
      return this.jwtHelper.decodeToken(token);
    }
    return null;
  }
}
