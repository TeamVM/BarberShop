import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { PriceComponent } from './price/price.component';
import { RegistrationComponent } from './registration/registration.component';
import { canActivate } from 'src/guards/auth.guard';
import { ContactComponent } from './contact/contact.component';
import { TermComponent } from './term/term.component';

const routes: Routes = [
  {
    path: 'register',
    component: RegistrationComponent,
  },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { 
    path: 'home', 
    component: HomeComponent,
  },
  {
    path: 'price',
    component: PriceComponent,
    canActivate: [canActivate]
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'contact',
    component: ContactComponent,
  },
  {
    path: 'term',
    component: TermComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
