import { Component, OnInit } from '@angular/core';
import { UserPageService } from './Services/user-page.service';
import { BrowserModule } from '@angular/platform-browser';

@Component({
  selector: 'app-userdisplay',
  templateUrl: './userDisplay.component.html',
  styleUrls: ['./userDisplay.component.css']
})
export class UserDisplayComponent implements OnInit{
  title = 'CropDApp';
  users! : any[];

  constructor(private userspageservice : UserPageService){

  }

  ngOnInit(): void {
    this.getallusers();
  }

  getallusers()
  {
    this.userspageservice.getallusers()
    .subscribe(
      response => {
            this.users = response;
      }
    )
  }

  changeStatus(userId:any,status:any){
    console.log('CLICKED')
    if(status=='Active'){
      status = 'Inactive'
    }else{
      status='Active'
    }
    this.userspageservice.changeUserStatus(userId,status).subscribe(res=>{
      console.log(res)
      this.getallusers()
    },
    err=>{
      if(err.status ==403 || err.status==401){
        alert('NO ACCESS ALLOWED')
      }
    })
  }
}
