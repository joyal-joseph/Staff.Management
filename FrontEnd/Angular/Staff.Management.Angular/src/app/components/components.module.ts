import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StaffComponent } from './staff/staff.component';
import { TeacherComponent } from './staff/teacher/teacher.component';
import { AdminComponent } from './staff/admin/admin.component';
import { SupportComponent } from './staff/support/support.component';
import { PaginationComponent } from './pagination/pagination.component';



@NgModule({
  declarations: [
    StaffComponent,
    TeacherComponent,
    AdminComponent,
    SupportComponent,
    PaginationComponent
  ],
  imports: [
    CommonModule
  ]
})
export class ComponentsModule { }
