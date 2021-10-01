import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StaffComponent } from './staff/staff.component';
import { TeacherComponent } from './staff/teacher/teacher.component';
import { AdminComponent } from './staff/admin/admin.component';
import { SupportComponent } from './staff/support/support.component';



@NgModule({
  declarations: [
    StaffComponent,
    TeacherComponent,
    AdminComponent,
    SupportComponent
  ],
  imports: [
    CommonModule
  ]
})
export class ComponentsModule { }
