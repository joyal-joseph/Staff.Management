import { RouterModule } from '@angular/router';
import { PaginationComponent } from './pagination/pagination.component';
import { NavigationComponent } from './navigation/navigation.component';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UpdateFormComponent } from './update-form/update-form.component';
import { ConfirmationBoxComponent } from './confirmation-box/confirmation-box.component';



@NgModule({
  declarations: [
    UpdateFormComponent,
    NavigationComponent,
    PaginationComponent,
    ConfirmationBoxComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule
    
  ],
  exports:[
    UpdateFormComponent,
    NavigationComponent,
    PaginationComponent,
    ConfirmationBoxComponent
  ]
})
export class CommonComponentModule { }
