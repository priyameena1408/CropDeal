import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { first } from 'rxjs';
import { AddCropDto } from 'src/app/Model/crop.model';
import { CropService } from 'src/app/crop.service';
import { UpdateCropDto } from 'src/app/Model/editCrop.model';

@Component({
  selector: 'app-edit-crop',
  templateUrl: './edit-crop.component.html',
  styleUrls: ['./edit-crop.component.css']
})
export class EditCropComponent implements OnInit {

  public ngForm !: FormGroup;

  submitted: boolean = false;
  id!:any
  constructor(private formBuilder: FormBuilder, 
    private api: CropService, 
    private router: Router,
     private toast: NgToastService,
     private _actRoute: ActivatedRoute) {
      this.id = this._actRoute.snapshot.paramMap.get("id");
      }

  crop:UpdateCropDto={
    CropType:'',
    CropName:'',
    CropLocation:'',
    FarmerId: 0,
    CropQtyAvailable:0,
    CropExpectedPrice:0
  }

  ngOnInit(): void {
    
    this.api.viewCropById(this.id)
    .subscribe(data => {
      this.ngForm = this.formBuilder.group({
        cropType: [data.value.cropType, Validators.required],
        cropName: [data.value.cropName, Validators.required],
        FarmerId: [localStorage.getItem('userId')],
        cropLocation: [data.value.cropLocation, Validators.required],
        CropQtyAvailable: [data.value.cropQtyAvailable, Validators.required],
        CropExpectedPrice: [data.value.cropExpectedPrice, Validators.required]
      });
    });
    
  }
  
  onSubmit() {
    this.submitted = true;
    if(this.ngForm.invalid){
      return;
    }
    console.log(this.ngForm.value)
    this.api.updateCrop(this.id,this.ngForm.value)
    .subscribe(data =>{
      console.log(data)
      this.toast.success({detail: "Success Message", summary: "Crop updated successfully", duration: 5000});
      this.ngForm.reset();
    }, err =>{
      this.toast.error({detail: "Error Message", summary: "Something went wrong while updating crop!!"+err.status, duration: 5000});
    })
  }

}
