import { Component, OnInit } from '@angular/core';
import { UserPageService } from '../Users Display/Services/user-page.service';
import { BrowserModule } from '@angular/platform-browser';

@Component({
  selector: 'app-reportpage',
  templateUrl: './reportpage.component.html',
  styleUrls: ['./reportpage.component.css']
})
export class ReportPageComponent implements OnInit{
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

  
}
