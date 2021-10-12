import { BaseComponent } from './../Base/base.class';
import { Component, OnInit } from '@angular/core';
import { Gender, StaffService } from '../../../services/all-staffs.service';
import { Admin } from 'src/app/Models/Admin.model';
//import { Admin } from 'src/app/Models/Admin.model';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent extends BaseComponent implements OnInit {

  title="List of Admins";

  constructor( _service: StaffService) {
    super(_service);
  }

  ngOnInit(): void {
    this.service.GetStaffs().subscribe((response)=>{
    
      (response as any[]).forEach(element => {
        if(element.jobType=="Admin"){
          element=element as Admin;
          this.staffs.push(element);
        }
      });
    });
  }

}
