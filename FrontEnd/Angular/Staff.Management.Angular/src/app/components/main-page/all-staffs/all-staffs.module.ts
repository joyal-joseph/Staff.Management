import { CommonComponentModule } from '../../common/common.module';
import { StaffService } from '../../../services/all-staffs.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AllStaffsComponent } from './all-staffs.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
      path:'', component: AllStaffsComponent
  }
]

@NgModule({
  declarations: [

    AllStaffsComponent
  ],
  imports: [
    CommonModule,
    CommonComponentModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    AllStaffsComponent,
  ],
  providers:[
StaffService  ]
})
export class AllStaffsModule { }

