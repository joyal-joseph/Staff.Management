import { ComponentsModule } from '../../components.module';
import { StaffService } from '../all-staffs/all-staffs.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeacherComponent } from './teacher.component';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
    {
        path:'', component: TeacherComponent
    }
]

@NgModule({
  declarations: [

    TeacherComponent
  ],
  imports: [
    CommonModule,
    ComponentsModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    TeacherComponent
  ],
  providers:[
StaffService  ]
})
export class TeacherModule { }