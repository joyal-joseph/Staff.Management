import { CommonComponentModule } from './../../common/common.module';
import { RouterModule, Routes } from '@angular/router';
import { StaffService } from '../../../services/all-staffs.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';

const routes: Routes = [
    {
        path:'', component: AdminComponent
    }
]

@NgModule({
  declarations: [
    AdminComponent
  ],
  imports: [
    CommonModule,
    CommonComponentModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    AdminComponent
  ],
  providers:[
StaffService  ]
})
export class AdminModule { }
