import { Component, OnInit } from '@angular/core';
import { User } from 'src/models/User';
import { AuthService } from 'src/service/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  isLoggedIn = false;
  isAdmin = false;
  currentUser: User;
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.currentUserChanged.subscribe(
      (user: User) => {
        this.currentUser = user;
      }
    )
  }


  logout() {
    this.authService.logout();
  }


}
