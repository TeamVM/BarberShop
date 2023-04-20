import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/service/auth.service';
import { Router } from '@angular/router';
import { catchError, finalize, tap, throwError } from 'rxjs';


@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {

  model: any = {};

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
  }


  register(form: NgForm) {
    if (this.isFormValid(form)) {
      this.authService.register(this.model).pipe(
        tap(() => {
          this.authService.login(this.model).pipe(
            tap(() => {
              this.router.navigate(['register']);
            }),
            catchError(error => {
              console.error('Login error:', error);
              return throwError(error);
            })
          ).subscribe();
        }),
        catchError(error => {
          console.error('Registration error:', error);
          return throwError(error);
        }),
        finalize(() => {
          // Ova funkcija se poziva kada se Observable završi ili kada dođe do greške
        })
      ).subscribe();
    }
  }

  onSubmit() {
    // Dodati logiku koja se izvršava kada se formular pošalje
    console.log('Form submitted!');
  }

  validateEmail(email: string): boolean {
    // Regex za proveru email adrese
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

    return emailRegex.test(email);
  }

  isFormValid(form: NgForm): boolean {
    if (!this.model.phone || this.model.phone.length < 8 || this.model.phone.length > 14) {
      console.log('Phone number is invalid!');
      return false;
    }
    if (form.valid && this.validateEmail(this.model.email)) {
      return true;
    } else {
      return false;
    }
  }
}