import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/auth.guard';
import { AddCropComponent } from './Components/add-crop/add-crop.component';
import { AdminHomeComponent } from './Components/admin-home/admin-home.component';
import { BuyerhomepageComponent } from './Components/buyerhomepage/buyerhomepage.component';
import { CropDetailsComponent } from './Components/crop-details/crop-details.component';
import { EditCropComponent } from './Components/edit-crop/edit-crop.component';
import { EditUserComponent } from './Components/edit-profile/editUser.component';
import { FarmerHomeComponent } from './Components/farmer-home/farmer-home.component';
import { InvoiceComponent } from './Components/invoice/invoice.component';
import { LoginComponent } from './Components/login/login.component';
import { PaymentSuccessfullComponent } from './Components/paysuccess/payment-successfull.component';
import { ProfileComponent } from './Components/profile/profile.component';
import { RegisterComponent } from './Components/register/register.component';
import { ReportPageComponent } from './Components/ReportPage/reportpage.component';
import { SubscriptionComponent } from './Components/subscription/subscription.component';
import { UserDisplayComponent } from './Components/Users Display/userDisplay.component';
import { ViewCropDealerComponent } from './Components/view-crop-dealer/view-crop-dealer.component';
import { ViewCropFarmerComponent } from './Components/view-crop-farmer/view-crop-farmer.component';
import { HasRoleGuard } from './has-role.guard';
import { ProfileCardComponent } from './profile-card/profile-card.component';

const routes: Routes = [
  {path:'login',component:LoginComponent},
  {path:'register',component:RegisterComponent},
  {path:'profile',component:ProfileComponent,canActivate:[AuthGuard]},
  {path:'farmerhome',component:FarmerHomeComponent,canActivate:[AuthGuard,HasRoleGuard],data:{
    role:'Farmer',
  }},
  {path:'add-crop',component:AddCropComponent,canActivate:[AuthGuard,HasRoleGuard],data:{
    role:'Farmer',
  }},
  {path:'editCrop/:id',component:EditCropComponent,canActivate:[AuthGuard,HasRoleGuard],data:{
    role:'Farmer',
  }},
  {path:'viewcropfarmer',component:ViewCropFarmerComponent,canActivate:[AuthGuard]},

  {path:'invoices',component:InvoiceComponent,canActivate:[AuthGuard]},
  {path:'dealerhome',component:BuyerhomepageComponent,canActivate:[AuthGuard,HasRoleGuard],data:{
    role:'Dealer',
  }},
  {path:'viewcropdealer',component:ViewCropDealerComponent,canActivate:[AuthGuard,HasRoleGuard],data:{
    role:'Dealer'
  }},
  
  {path:'adminhome',component:AdminHomeComponent,canActivate:[AuthGuard,HasRoleGuard],data:{
    role:'Admin',
  }},
  {path:'userStatus',component:UserDisplayComponent,canActivate:[AuthGuard,HasRoleGuard],data:{
    role:'Admin',
  }},
  {path:'reports',component:ReportPageComponent,canActivate:[AuthGuard,HasRoleGuard],data:{
    role:'Admin'
  }},

  {path:'crop-detail/:id',component:CropDetailsComponent,canActivate:[AuthGuard]},
  {path:'editProfile',component:EditUserComponent,canActivate:[AuthGuard]},
  
  {path:'paysuccess',component:PaymentSuccessfullComponent},
  {path:'subs',component:SubscriptionComponent,canActivate:[AuthGuard,HasRoleGuard],data:{
    role:'Dealer',
  }},
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
