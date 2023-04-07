import { Injectable } from "@angular/core";
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';


@Injectable({
    providedIn: 'root'
})
export class RegstrationService{
    baseUrl = environment.apiUrl + 'user/';

    constructor(private http: HttpClient){}
    
    
    register(model: any) {
        return this.http.post(this.baseUrl + 'register', model);
     }
     
}

