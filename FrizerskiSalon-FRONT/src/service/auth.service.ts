import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { User } from 'src/models/User';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    baseUrl = environment.apiUrl + 'user/';
    jwtHelper = new JwtHelperService();
    decodedToken: any;
    currentUser!: User;
    currentRole: string = '';

    constructor(private http: HttpClient, private router: Router) { }

    login(model: any) {
        return this.http.post(this.baseUrl + 'login', model)
            .pipe(
                map((response: any) => {
                    const user = response;
                    if (user) {
                        localStorage.setItem('token', user.token);
                        localStorage.setItem('user', JSON.stringify(user.user));
                        this.decodedToken = this.jwtHelper.decodeToken(user.token);
                        this.currentUser = user.user;
                        this.router.navigate(['home']);
                    }
                })
            );
    }

    register(model: any) {
        return this.http.post(this.baseUrl + 'register', model);
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
