import { Component, ElementRef, ViewChild,OnInit} from '@angular/core';
import jsPDF from 'jspdf';
import html2canvas  from 'html2canvas';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { ThisReceiver } from '@angular/compiler';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css']
})
export class InvoiceComponent implements OnInit {
  
  id!:string|null
  role!:string|null
  userid:any;
  @ViewChild('invoice') invoiceElement!: ElementRef;
  log=1;
 
  title = 'angularpdfgenerator';
 
  invoices!:any[]

  constructor(private http:HttpClient,private active:ActivatedRoute) { 
    if(localStorage.getItem('role')=='Admin'){
      console.log('inside admin')
      this.role = this.active.snapshot.paramMap.get('role')
      this.id = this.active.snapshot.paramMap.get('id')
    }else{
      this.role=localStorage.getItem('role')
      this.id=localStorage.getItem('userId')
    }
    this.getInvoices()
  }
 
  ngOnInit(): void {

  }
 
  getInvoices(){
    
    if(this.role == 'Farmer'){
        this.http.get('https://localhost:44346/api/Invoice/farmerInvoices/'+this.id).subscribe((res:any)=>{
          this.invoices = res;
          console.log(this.invoices)
        })
    }
    else if(this.role == 'Dealer'){
      this.http.get('https://localhost:44346/api/Invoice/dealerInvoices/'+this.id).subscribe((res:any)=>{
        this.invoices = res;
        console.log(this.invoices)
        
    })
  }}

  public generatePDF(): void {

      html2canvas(this.invoiceElement.nativeElement, { scale: 3 }).then((canvas) => {
        const imageGeneratedFromTemplate = canvas.toDataURL('image/png');
        const fileWidth = 200;
        var pageHeight =300 ;
        const generatedImageHeight = (canvas.height * fileWidth) / canvas.width;
        var heightLeft = generatedImageHeight;
        let PDF = new jsPDF('p', 'mm', );
        var position = 0;



        PDF.addImage(imageGeneratedFromTemplate, 'PNG', 0, position, fileWidth, generatedImageHeight,);
        heightLeft -= pageHeight;
        while (heightLeft >= 0) {
          position = heightLeft - generatedImageHeight;
          PDF.addPage();
          PDF.addImage(imageGeneratedFromTemplate, 'PNG', 0, position, fileWidth, generatedImageHeight);
          heightLeft -= pageHeight;
        }
        
        PDF.save('Invoice.pdf');
      });
  }
}
