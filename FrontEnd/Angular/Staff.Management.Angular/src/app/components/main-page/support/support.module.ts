import { StaffService } from '../../../services/all-staffs.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SupportComponent } from './support.component';
import { RouterModule, Routes } from '@angular/router';
import { CommonComponentModule } from '../../common/common.module';

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
    CommonComponentModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    SupportComponent
  ],
  providers:[
StaffService  ]
})
export class SupportModule { }