import { Component, OnInit } from '@angular/core';
import { FormGroup,FormBuilder,Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/Services/api.service';
import { User } from 'src/app/Model/User';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;
  submitted:boolean = false;
  result!:any
  roles = [ 'Farmer', 'Dealer','Admin'];
  show_button: Boolean = false;
  show_eye: Boolean = false;
  user = new User();
  constructor(private router:Router, private formBuilder:FormBuilder ,public api:ApiService, private toast: NgToastService) { }

  ngOnInit(): void {   
    this.loginForm = this.formBuilder.group({
      email:  ['', [Validators.required,Validators.email]],
      password:  ['', [Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}')]],
      userRole:['', Validators.required] 
  });
  

  }
  onSubmit(){
    this.submitted = true;
    if(this.loginForm.invalid){
      return;
    }
    else{
      this.user.role=this.loginForm.controls['userRole'].value;
      this.user.userName=this.loginForm.controls['email'].value;
      this.user.password=this.loginForm.controls['password'].value;
      this.api.login(this.user).subscribe( res => {
        this.result = res
        localStorage.setItem('role',this.result.role)
        localStorage.setItem('userId',this.result.userId)
        localStorage.setItem('token',this.result.token)
        const token =  localStorage.getItem('token')?.toString()!
        const role = (Object.values(JSON.parse(atob(token.split('.')[1]))).at(1))
        if(role == 'Farmer'){
          // alert('Logged in Successfully')
          this.toast.success({detail: "Success Message", summary: "Logged in Successfully", duration: 5000});
          this.router.navigate(['/farmerhome']).then(() => {
            window.location.reload();
          });
         
        }else if(role == 'Dealer'){
          // alert('Logged in Successfully')
          this.toast.success({detail: "Success Message", summary: "Logged in Successfully", duration: 5000});
          this.router.navigate(['/dealerhome']).then(() => {
            window.location.reload();
          });
         
        }
        else if(role == 'Admin'){
          // alert('Logged in Successfully')
          this.toast.success({detail: "Success Message", summary: "Logged in Successfully", duration: 5000});
          this.router.navigate(['/adminhome']).then(() => {
            window.location.reload();
          });
          
        }
      },
      err=>{
        if(err.status==401){
          alert('Wrong Password')
        }else if(err.status==400){
          alert('Inactive user login')
        }
        else if(err.status==404){
          alert('User not Found!! Register or check Email again')
        }
      });

    }
    
    
   }
  gohome(){this.router.navigate(['register']);}
  get f() { return this.loginForm.controls; }
  showPassword() {
    this.show_button = !this.show_button;
    this.show_eye = !this.show_eye;
}

}
