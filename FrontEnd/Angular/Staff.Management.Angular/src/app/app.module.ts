import { NotFoundComponent } from './components/staffs/not-found/not-found.component';

import {AllStaffsModule } from './components/staffs/all-staffs/all-staffs.module';
import { SupportModule } from './components/staffs/support/support.module';
import { AdminModule } from './components/staffs/admin/admin.module';
import { TeacherModule } from './components/staffs/teacher/teacher.module';
import { RouterModule } from '@angular/router';
import { NavigationComponent } from './components/main-page/navigation/navigation.component';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
// import { StaffsModule } from './components/staffs/staffs.module';
import { StaffService } from './components/staffs/all-staffs/all-staffs.service';
import { AllStaffsComponent } from './components/staffs/all-staffs/all-staffs.component';

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent
    ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      {
        path:'AllStaff',
        component: AllStaffsComponent
      },
      {
        path:'Teacher',
        loadChildren: () => import('./components/staffs/teacher/teacher.module').then( m => m.TeacherModule)
      },
      {
        path:'Support',
        loadChildren: () => import('./components/staffs/support/support.module').then( m => m.SupportModule)
      },
      {
        path:"Admin",
        loadChildren: () => import('./components/staffs/admin/admin.module').then( m => m.AdminModule)
      },
      {
        path:"**",
        component: NotFoundComponent
      }
    ])
  ],
  providers: [
    StaffService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
