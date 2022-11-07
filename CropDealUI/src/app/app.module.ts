import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import {  HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { ProfileComponent } from './Components/profile/profile.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FooterComponent } from './Components/footer/footer.component';
import { NavbarComponent } from './Components/navbar/navbar.component';
import { AddCropComponent } from './Components/add-crop/add-crop.component';
import { AuthInterceptor } from './token-interceptor.service';
import { AuthGuard } from 'src/auth.guard';
import { HasRoleGuard } from './has-role.guard';
import { InvoiceComponent } from './Components/invoice/invoice.component';
import { FarmerHomeComponent } from './Components/farmer-home/farmer-home.component';
import { BuyerhomepageComponent } from './Components/buyerhomepage/buyerhomepage.component';
import { ViewCropFarmerComponent } from './Components/view-crop-farmer/view-crop-farmer.component';

import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {  MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import { MatIconModule} from '@angular/material/icon';
import { MatDividerModule} from '@angular/material/divider'
import { UserDisplayComponent } from './Components/Users Display/userDisplay.component';
import { CropDetailsComponent } from './Components/crop-details/crop-details.component';
import { EditUserComponent } from './Components/edit-profile/editUser.component';
import { EditCropComponent } from './Components/edit-crop/edit-crop.component';
import { ViewCropDealerComponent } from './Components/view-crop-dealer/view-crop-dealer.component';
import { ReportPageComponent } from './Components/ReportPage/reportpage.component';
import { PaymentSuccessfullComponent } from './Components/paysuccess/payment-successfull.component';
import { CropService } from './crop.service';
import {Ng2SearchPipeModule} from 'ng2-search-filter';
import { SubscriptionComponent } from './Components/subscription/subscription.component';
import { NgToastModule } from 'ng-angular-popup';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    FooterComponent,
    NavbarComponent,
    AddCropComponent,
    InvoiceComponent,
    FarmerHomeComponent,
    BuyerhomepageComponent,
    ViewCropFarmerComponent,
    UserDisplayComponent,
    AddCropComponent ,
    CropDetailsComponent,
    EditUserComponent,
    EditCropComponent,
    ViewCropDealerComponent,
    ReportPageComponent,
    PaymentSuccessfullComponent,
    SubscriptionComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatSidenavModule,
    MatButtonModule,
    MatDividerModule,
    MatToolbarModule,
    BrowserAnimationsModule,
    MatIconModule,
    Ng2SearchPipeModule,
    FormsModule,
    NgToastModule
  ],
  providers: [{
    provide:HTTP_INTERCEPTORS,
    useClass:AuthInterceptor,
    multi:true
  },
  AuthGuard,
  HasRoleGuard,
  
],
  bootstrap: [AppComponent]
})
export class AppModule { }
