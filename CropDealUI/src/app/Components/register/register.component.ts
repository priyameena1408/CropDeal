import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder ,Validators} from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/Services/api.service';
import { RegisterUser } from 'src/app/Model/RegisterUser';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  
  profileForm!: FormGroup;
  submitted:boolean = false;
  roles = [ 'Farmer', 'Dealer'];
  show_button: Boolean = false;
  show_eye: Boolean = false;
  newUser = new RegisterUser();

 
  constructor(private router:Router, private formBuilder:FormBuilder , public api:ApiService, private toast: NgToastService) { }

  ngOnInit(): void {   
    this.profileForm = this.formBuilder.group({
    name:  ['', [Validators.required,Validators.pattern('^[A-Za-z]{3,}$')]],
    email:  ['', [Validators.required,Validators.email]],
    password:  ['', [Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}')]],
    phoneNo:  ['',  [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]],
    address:  ['',Validators.required],
    city:  ['',[Validators.required,Validators.pattern('^[A-Za-z]+$')]],
    state:  ['',[Validators.required,Validators.pattern('^[A-Za-z]+$')]],
    accountNumber:  ['',[Validators.required,Validators.pattern('^[1-9]{1}[0-9]{8,17}$')]],
    ifscCode:  ['',[Validators.required,Validators.pattern('^[A-Z]{4}0[A-Z0-9]{6}$')]],
    bankName: ['',[Validators.required,Validators.pattern('^[A-Za-z]+$')]],
    userRole:['', [Validators.required]],
 
  });
  }

  onSubmit():any{
    this.submitted = true;
    console.log(this.profileForm.value)
    if(this.profileForm.invalid){
        return;
    }
    else{
      this.newUser.Name=this.profileForm.controls['name'].value;
      this.newUser.Role=this.profileForm.controls['userRole'].value;
      this.newUser.Email=this.profileForm.controls['email'].value;
      this.newUser.Password=this.profileForm.controls['password'].value;
      this.newUser.Phone=this.profileForm.controls['phoneNo'].value;
      this.newUser.Line=this.profileForm.controls['address'].value;
      this.newUser.City=this.profileForm.controls['city'].value;
      this.newUser.State=this.profileForm.controls['state'].value;
      this.newUser.AccountNumber=this.profileForm.controls['accountNumber'].value;
      this.newUser.IFSC =this.profileForm.controls['ifscCode'].value;
      this.newUser.BankName=this.profileForm.controls['bankName'].value;
      this.api.register(this.newUser)
      .subscribe( res => {
        this.toast.success({detail: "Success Message", summary: "Registerd Successfully", duration: 5000});
        console.log(res);
    });
    
    }
    this.router.navigate(['login']);
    
   }
  gohome(){this.router.navigate(['login']);}
  
  get f() { return this.profileForm.controls; }
  
  showPassword() {
    this.show_button = !this.show_button;
    this.show_eye = !this.show_eye;
}



}
