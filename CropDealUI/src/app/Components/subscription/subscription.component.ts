import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-subscription',
  templateUrl: './subscription.component.html',
  styleUrls: ['./subscription.component.css']
})
export class SubscriptionComponent implements OnInit {

  constructor(private http: HttpClient) { }
//+localStorage.getItem('userId')
  subs!: any[]

  
  ngOnInit(): void {
    this.getSubs();
  } 

  subscribe(cid:any){
    this.http.post('https://localhost:44346/api/Subscription/addSubs/'+localStorage.getItem('userId'),cid).subscribe(
      res=>{
        //console.log(res)
        this.getSubs();
      },
      err=>{
        alert(err.message)
      }
    )
  }

  unsubscribe(cid:any){
    this.http.delete('https://localhost:44346/api/Subscription/deleteSubs/'+localStorage.getItem('userId'),{"body":cid}).subscribe(
      res=>{
        //console.log(res)
        this.getSubs();
      },
      err=>{
        alert(err.message)
      }
    )
  }
  
  getSubs(){
    this.http.get('https://localhost:44346/api/Subscription/getSubs/'+localStorage.getItem('userId'))
    .subscribe((res:any)=>{
      this.subs = res.map((p:any)=>p.cropTypeId)
      console.log(this.subs)
      //console.log(this.subs.find(p=>p==1)>-1)
    })
  }

  isSubscribe(id:Number):Boolean{
    if(this.subs.find(p=>p==id)>-1){
      return true
    }
    return false
  }
}
