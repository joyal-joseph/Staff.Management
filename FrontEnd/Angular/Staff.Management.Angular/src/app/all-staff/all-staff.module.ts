
import { Support } from './support';
import { Admin } from './admin';
import { Teacher } from './teacher';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Staff } from './staff';



@NgModule({
  declarations: [
  
    Teacher,
    Admin,
    Support
  ],
  imports: [
    CommonModule
  ]
})
export class AllStaffModule { }
