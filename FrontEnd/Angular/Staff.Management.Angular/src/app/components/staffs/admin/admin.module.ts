import { RouterModule, Routes } from '@angular/router';
import { ComponentsModule } from '../../components.module';
import { StaffService } from '../all-staffs/all-staffs.service';
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
    ComponentsModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    AdminComponent
  ],
  providers:[
StaffService  ]
})
export class AdminModule { }
