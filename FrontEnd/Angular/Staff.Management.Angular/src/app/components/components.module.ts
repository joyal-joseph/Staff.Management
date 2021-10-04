import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationComponent } from './pagination/pagination.component';
import { AllStaffsComponent } from './staffs/all-staffs/all-staffs.component';
import { NotFoundComponent } from './staffs/not-found/not-found.component'
// import { StaffsModule } from './staffs/staffs.module';



@NgModule({
  declarations: [
    PaginationComponent,
    NotFoundComponent,
  ],
  imports: [
    CommonModule
  ],
  exports:[
    PaginationComponent,
  ]
})
export class ComponentsModule { }
