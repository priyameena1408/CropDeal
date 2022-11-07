import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AddCropDto } from 'src/app/Model/crop.model';
import { CropService } from 'src/app/crop.service';
import { NgToastService } from 'ng-angular-popup'
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-add-crop',
  templateUrl: './add-crop.component.html',
  styleUrls: ['./add-crop.component.css']
})
export class AddCropComponent implements OnInit {

  public addForm !: FormGroup;

  submitted: boolean = false;

  constructor(private formBuilder: FormBuilder,private http:HttpClient ,private api: CropService, private router: Router, private toast: NgToastService) { }

  crop:AddCropDto={
    CropType:'',
    CropName:'',
    fid:0,
    CropLocation:'',
    CropQtyAvailable:0,
    CropExpectedPrice:0

  }
  ngOnInit(): void {
    this.addForm = this.formBuilder.group({
      cropType: ['', Validators.required],
      cropName: ['', Validators.required],
      cropLocation: ['', Validators.required],
      qtyAvailable: ['', Validators.required],
      expectedPrice: ['', Validators.required]
    });
  }
  selectedFile!:File
  onFileSelected(event:any){
    this.selectedFile = <File>event.target.files[0];
  }

  // onUpload(){
  //   const filedata = new FormData();
  //   filedata.append('image',this.selectedFile,this.selectedFile.name);
  //   console.log(filedata);
  // }

  onSubmit() {
    this.submitted = true;
    if(this.addForm.invalid){
      return;
    }
    this.crop.CropType = this.addForm.controls['cropType'].value;
    this.crop.CropName = this.addForm.controls['cropName'].value;
    this.crop.fid = Number(localStorage.getItem('userId'));
    this.crop.CropLocation= this.addForm.controls['cropLocation'].value;
    this.crop.CropQtyAvailable = this.addForm.controls['qtyAvailable'].value;
    this.crop.CropExpectedPrice = this.addForm.controls['expectedPrice'].value;
    
    this.api.addCrop(this.crop)
    .subscribe((response:any)=>{
    console.log(response); 

     const filedata = new FormData();
     filedata.append('image',this.selectedFile,this.selectedFile.name);
     this.http.put('https://localhost:44346/api/Crop/cropimg/'+response.value.cropId,filedata).subscribe(
      (res:any)=>{
        console.log(res)
        this.router.navigate(['farmerhome'])
      },
      err=>{
        console.log(err)
      }
     )
    
    }, error =>{
      if(error.status==401 || error.status==403){
        alert('Unauthorized')
      }
      console.log(error);
      this.toast.error({detail: "Error Message", summary: "Something went wrong!!", duration: 5000});
    })

   
  }

}





