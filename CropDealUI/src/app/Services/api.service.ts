import { Injectable } from '@angular/core';
import { User } from '../Model/User';
import { HttpClientModule,HttpClient } from  '@angular/common/http';
import { RegisterUser } from '../Model/RegisterUser';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
 

  constructor(private http:HttpClient) {}


  login(user:User){
    return this.http.post('https://localhost:44346/api/login',user);
  }
  register(newUser: RegisterUser) {
    return this.http.post('https://localhost:44346/api/User/register',newUser);
  }
}
