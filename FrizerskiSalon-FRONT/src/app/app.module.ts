import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegistrationComponent } from './registration/registration.component';
import { HomeComponent } from './home/home.component';
import { PriceComponent } from './price/price.component';
import { LoginComponent } from './login/login.component';
import { AuthService } from 'src/service/auth.service';
import { JwtModule } from '@auth0/angular-jwt';
import { TermComponent } from './term/term.component';
import { ContactComponent } from './contact/contact.component';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';
import { TermCardComponent } from './term-card/term-card.component';


@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent,
    HomeComponent,
    PriceComponent,
    LoginComponent,
    TermComponent,
    ContactComponent,
    TermCardComponent,
  ],
  imports: [
    LeafletModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
       config: {
          tokenGetter,
          allowedDomains: ['localhost:5125'],
          disallowedRoutes: ['localhost:5125/api/auth']
       }
    })
  ],
  providers: [
    AuthService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }


export function tokenGetter() {
  return localStorage.getItem('token');
}