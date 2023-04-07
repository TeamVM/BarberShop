import { EmailValidator } from "@angular/forms";

export class Worker{
    userID: number;
    name: string;
    surname: string;
    phone: number;
    email: string;


    constructor(userID: number, name: string, surname: string, phone: number, email: string){
        this.userID = userID;
        this.name = name;
        this.surname = surname;
        this.phone = phone;
       this.email = email; 
    }
}