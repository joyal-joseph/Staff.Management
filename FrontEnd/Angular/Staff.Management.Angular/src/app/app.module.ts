import { CommonComponentModule } from './components/common/common.module';
import { NotFoundComponent } from './components/main-page/not-found/not-found.component';

import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { StaffService } from './services/all-staffs.service';

@NgModule({
  declarations: [
    AppComponent,
    ],
  imports: [
    BrowserModule,
    CommonComponentModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      {
        path:'AllStaff',
        loadChildren: () => import('./components/main-page/all-staffs/all-staffs.module').then( m => m.AllStaffsModule)
      },
      {
        path:'Teacher',
        loadChildren: () => import('./components/main-page/teacher/teacher.module').then( m => m.TeacherModule)
      },
      {
        path:'Support',
        loadChildren: () => import('./components/main-page/support/support.module').then( m => m.SupportModule)
      },
      {
        path:"Admin",
        loadChildren: () => import('./components/main-page/admin/admin.module').then( m => m.AdminModule)
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
