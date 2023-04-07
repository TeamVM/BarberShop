import { Component } from '@angular/core';
import { RegstrationService } from 'src/service/registration.service';
import { NgModel } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
      model: any = {};

      constructor(private regService: RegstrationService){}

      ngOnInit() {
      }

      register(){
        this.regService.register(this.model).subscribe(() =>{
        });
      }

      onSubmit() {
        // Dodati logiku koja se izvršava kada se formular pošalje
        console.log('Form submitted!');
      }
}
