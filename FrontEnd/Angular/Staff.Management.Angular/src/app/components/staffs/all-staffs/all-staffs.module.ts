import { ComponentsModule } from '../../components.module';
import { StaffService } from './all-staffs.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AllStaffsComponent } from './all-staffs.component';


@NgModule({
  declarations: [

    AllStaffsComponent
  ],
  imports: [
    CommonModule,
    ComponentsModule
  ],
  exports:[
    AllStaffsComponent
  ],
  providers:[
StaffService  ]
})
export class AllStaffsModule { }