import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GetUser } from 'src/app/Model/GetUser';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  user = new GetUser()
  constructor(private router:Router,private http:HttpClient) {
    
  }
  rating!:any
  city!:any
  role = localStorage.getItem('role')
  ngOnInit(): void { 
    this.http.get('https://localhost:44346/api/User/getuser/'+localStorage.getItem('userId')).subscribe((res:any)=>{
       this.user.name = res.value.name
       this.user.email=res.value.email
       this.user.phone=res.value.phone
       this.user.accnumber=res.value.account.accountNumber
       this.user.bankname=res.value.account.bankName
       this.user.ifsc=res.value.account.ifscCode

       this.user.address = res.value.address.line+' '+res.value.address.city+' '+res.value.address.state
       
     
    });

    this.http.get('https://localhost:44346/api/User/getRating/'+localStorage.getItem('userId')).subscribe(
      res=>{
        this.rating = res
      }
    )
  }
  goHome(){this.router.navigate(['register']);}
  
}
 