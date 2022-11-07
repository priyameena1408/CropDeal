import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EditUserService } from './EditUser.service';
import { UserPageDto } from './UserPageDto';
import { UpdateUserDto } from 'src/app/Model/UpdateUserDto';

@Component({
  selector: 'app-profile',
  templateUrl: './editUser.component.html',
  styleUrls: ['./editUser.component.css']
})
export class EditUserComponent implements OnInit {
  
  
  EdituserForm!: FormGroup;
  public userdto : UserPageDto = new UserPageDto();
  submitted:boolean = false;
  constructor(private router:Router,private edituserservice : EditUserService,private formBuilder:FormBuilder
    ,private http : HttpClient) {

      this.http.get('https://localhost:44346/api/User/getuser/'+localStorage.getItem('userId'))
      .subscribe((res:any)=>{
        this.EdituserForm = this.formBuilder.group({
       
          email:  [res.value.email, [Validators.required,Validators.email]],
          phone:  [res.value.phone,  [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]],
          line:  [res.value.address.line, Validators.required],
          city:  [res.value.address.city, Validators.required],
          state:  [res.value.address.state, Validators.required],
          accountNumber:  [ res.value.account.accountNumber, Validators.required],
          IFSC:  [res.value.account.ifscCode, Validators.required],
          bankName: [ res.value.account.bankName, Validators.required],
    
    }); 
      });
     }

  crop!:any
  ngOnInit(): void {  

  }
  
  userId = Number(localStorage.getItem('userId'))
  get f() { return this.EdituserForm.controls; }
 
  goHome(){this.router.navigate(['']);}

  SaveChanges(){
    console.log('Clicked')
    this.submitted = true;
    console.log(this.EdituserForm.value)
    this.edituserservice.edituser(this.EdituserForm.value, this.userId )
    .subscribe(
      response => {
        console.log(response)
        this.router.navigate(['/profile'])
      }
    );
  }
}
