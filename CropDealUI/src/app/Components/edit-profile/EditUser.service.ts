import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserPageDto } from './UserPageDto';

@Injectable({
  providedIn: 'root'
})
export class EditUserService {

  private baseurl = "https://localhost:44346/";
  constructor(private http: HttpClient) { }

  public edituser(user : UserPageDto, id : number){
    return this.http.put(this.baseurl+'api/User/updateUser/'+id, user);
  }
}