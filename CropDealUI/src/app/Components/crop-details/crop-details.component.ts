import { Component, OnInit } from '@angular/core';
import { getAllCrop } from 'src/app/Model/getAllCrop.model';
import { CropService } from 'src/app/crop.service';
import { Payment } from 'src/app/Model/Payment';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-crop-details',
  templateUrl: './crop-details.component.html',
  styleUrls: ['./crop-details.component.css']
})
export class CropDetailsComponent implements OnInit {

  id!:any
  
  constructor(private cropService: CropService,private _actRoute: ActivatedRoute,private router: Router) {
    this.id = this._actRoute.snapshot.paramMap.get("id");
    this.GetCropById(this.id);
   }

  ngOnInit(): void {
    
  }
  
  pay = new Payment()

  singleCrop!:any
  GetCropById(id:any){
    this.cropService.viewCropById(id).subscribe(res=>{
      this.singleCrop= res.value
      console.log(this.singleCrop)
    })
  }

  onPay(){
    this.pay.cropId=Number(this.id);
    this.pay.farmerId= this.singleCrop.farmerId
    this.pay.dealerId= Number(localStorage.getItem('userId'))
    console.log(this.pay)
    this.cropService.postInvoice(this.pay).subscribe((res:any)=>{
      console.log(res)
      alert('Payment Successful')
      this.router.navigate(['/paysuccess',{uid:res.farmerId}])
    },err=>{
      console.log(err)
    })
  }
  
  

}
