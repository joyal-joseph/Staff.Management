//import { Support } from 'src/app/Models//Support.model';
import { BaseComponent } from './../Base/base.class';
import { Component, OnInit } from '@angular/core';
import { Gender, StaffService } from '../../../services/all-staffs.service';
import { Support } from 'src/app/Models/Support.model';

@Component({
  selector: 'app-support',
  templateUrl: './support.component.html',
  styleUrls: ['./support.component.css']
})
export class SupportComponent extends BaseComponent implements OnInit {

  title="List of Support Staffs";  

  constructor(private _service: StaffService) { 
    super(_service);
  }

  ngOnInit(): void {
    this.service.GetStaffs().subscribe((response)=>{
      (response as any[]).forEach(element => {
        if(element.jobType=="Support"){
          element= element as Support;
          this.staffs.push(element);
        }
      });
    });
  }
}
