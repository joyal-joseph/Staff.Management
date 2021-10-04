import { ComponentsModule } from '../../components.module';
import { StaffService } from '../all-staffs/all-staffs.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SupportComponent } from './support.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path:'', component: SupportComponent
    }
]


@NgModule({
  declarations: [

    SupportComponent
  ],
  imports: [
    CommonModule,
    ComponentsModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    SupportComponent
  ],
  providers:[
StaffService  ]
})
export class SupportModule { }