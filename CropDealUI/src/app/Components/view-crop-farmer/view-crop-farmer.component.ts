import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { CropService } from 'src/app/crop.service';


@Component({
  selector: 'app-view-crop-farmer',
  templateUrl: './view-crop-farmer.component.html',
  styleUrls: ['./view-crop-farmer.component.css']
})
export class ViewCropFarmerComponent implements OnInit {

  cropForm: any[] =[];
 

  selectedCropNumber: string="Top 10 Trending Crops!";
  
  constructor(private cd : ChangeDetectorRef, private cropService : CropService) {} 

  ngOnInit(): void {
    this.getAllCrops();
  }

  getAllCrops(){
    this.cropService.getAllCrops()
    .subscribe(
      response =>{
        this.cropForm= response;
        //console.log(this.cropForm)
        this.cropForm=this.cropForm.filter(res=>res.farmerId==Number(localStorage.getItem('userId')))
      }
    )
  }

}