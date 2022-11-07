import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'CropDeal';

  //allUser!: any[]
  constructor(private http:HttpClient){}
  ngOnInit(){
    //this.getUsers();
  }

  // getUsers(){
  //   this.http.get<any[]>('https://localhost:44346/api/User/getUsers').subscribe(res=>{
  //     this.allUser = res
  //     console.log(res);
  //   })
  // }

}

