import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { User } from 'src/models/User';
import { Observable, Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    baseUrl = environment.apiUrl + 'user/';
    jwtHelper = new JwtHelperService();
    decodedToken: any;
    currentUser?: User;
    currentRole: string = '';

    currentUserChanged = new Subject<User>();

    constructor(private http: HttpClient, private router: Router) { }

    login(email: string, password: string): Observable<User> {

        let model = {
            email,
            password
        }

        return this.http.post(this.baseUrl + 'login', model)
            .pipe(
                map((response: any) => {
                    if (response) {
                        localStorage.setItem('token', response.token);
                        localStorage.setItem('user', JSON.stringify(response.user));
                        this.decodedToken = this.jwtHelper.decodeToken(response.token);
                        this.currentUser = response.user;
                        this.router.navigate(['home']);
                    }
                    this.currentUserChanged.next(this.currentUser);
                    return this.currentUser;
                })
            );
    }

    register(model: any) {
        return this.http.post(this.baseUrl + 'register', model);
    }

    logout() {
        this.decodedToken = null;
        this.currentUser = null;
        this.currentRole = null;
        localStorage.removeItem('token');
        localStorage.removeItem('user');
        localStorage.removeItem('role');
        localStorage.removeItem('email');
        this.currentUserChanged.next(null);
    }

    loggedIn() {
        let token = localStorage.getItem('token');
        if (token) {
            this.decodedToken = this.jwtHelper.decodeToken(token);

        }

        return !this.jwtHelper.isTokenExpired(token);
    }

    isAdmin() {
        let token = localStorage.getItem('token');
        if (token) {
            let decodedToken = this.jwtHelper.decodeToken(token);
            return decodedToken.role === 'Admin';
        }
        return false;
    }
}
