import { PaginationComponent } from './components/pagination/pagination.component';
import { NavigationComponent } from './components/main-page/navigation/navigation.component';
import { StaffService } from './components/staff/staff.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StaffComponent } from './components/staff/staff.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    StaffComponent,
    NavigationComponent,
    PaginationComponent   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [
    StaffService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
