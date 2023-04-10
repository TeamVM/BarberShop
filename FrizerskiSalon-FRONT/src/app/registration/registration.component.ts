import { Component } from '@angular/core';
import { RegstrationService } from 'src/service/registration.service';
import { NgModel } from '@angular/forms';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})


export class RegistrationComponent {
  myForm!: FormGroup;    
  model: any = {};
  
      constructor(private regService: RegstrationService, private formBuilder:FormBuilder){}

      ngOnInit() {
        this.myForm = this.formBuilder.group({
          name: ['', Validators.required]
        })
      }

      register() {
        if (this.myForm.valid) {
          this.regService.register(this.model).subscribe(() =>{
          });
        } else {
          console.log("Form is invalid");
        }
      }

      onSubmit() {
        // Dodati logiku koja se izvršava kada se formular pošalje
        console.log('Form submitted!');
      }
}
