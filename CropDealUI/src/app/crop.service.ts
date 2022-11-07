import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { AddCropDto } from './Model/crop.model';
import { getAllCrop } from './Model/getAllCrop.model';
import { UpdateCropDto } from './Model/editCrop.model';

@Injectable({
  providedIn: 'root'
})
export class CropService {

  baseUrl:string = 'https://localhost:44346/api/Crop/';
  

  constructor(private http: HttpClient) { }

  addCrop(crop: AddCropDto) {
    return this.http.post(this.baseUrl+'addCrop', crop);
  }

  viewCropById(id: number){
    return this.http.get<any>(this.baseUrl+'viewCrop/'+id);

  }

  getAllCrops(): Observable<getAllCrop[]>{
    return this.http.get<getAllCrop[]>(this.baseUrl+'getCrops')
  }

  getCropById(id:any){
    return this.http.get(this.baseUrl+'getCrops/'+id)
  }

  postInvoice(payment:any){
    return this.http.post('https://localhost:44346/api/Invoice/addInvoice',payment);
  }

  updateCrop(id:any,crop:UpdateCropDto){
    return this.http.put(this.baseUrl+'editCrop/'+id,crop)
  }
}
