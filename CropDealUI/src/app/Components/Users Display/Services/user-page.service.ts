import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserPageService {

  private baseurl = "https://localhost:44346/";
  constructor(private http: HttpClient) { }

  public getallusers() : Observable<any[]>{
    return this.http.get<any[]>(this.baseurl+'api/User/getUsers');
  }

  public changeUserStatus(userId:any,stat:string){
    return this.http.put(this.baseurl+'api/User/status/'+userId+'?stat='+stat,null);
  }

}
