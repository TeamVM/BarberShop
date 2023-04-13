import { Component } from '@angular/core';
import { AuthService } from 'src/service/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  model: any = {};

  constructor(private authService: AuthService) { }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('Logged in successfully!');

    }, error => {
      console.error(error);
    });
  }
}
