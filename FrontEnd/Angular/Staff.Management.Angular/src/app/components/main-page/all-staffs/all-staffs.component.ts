// import { Support } from 'src/app/Models/Support.model';
// import { Admin } from 'src/app/Models/Admin.model';
// import { Teacher } from 'src/app/Models/Teacher.model';
import { BaseComponent } from '../Base/base.class';

import { NgIf } from '@angular/common';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { isDelegatedFactoryMetadata } from '@angular/compiler/src/render3/r3_factory';
import { Component, OnInit, Output,EventEmitter } from '@angular/core';
import { from, Observable, of } from 'rxjs';
import { StaffService, Gender } from '../../../services/all-staffs.service';
import { Teacher } from 'src/app/Models/Teacher.model';
import { Admin } from 'src/app/Models/Admin.model';
import { Support } from 'src/app/Models/Support.model';



@Component({
  selector: 'app-all-staffs',
  templateUrl: './all-staffs.component.html',
  styleUrls: ['./all-staffs.component.css']
})


export class AllStaffsComponent extends BaseComponent implements OnInit {
  title="List of all staffs";
  
  constructor(private _service: StaffService )  {
    super(_service);
  }

  ngOnInit(): void {
    this.service.GetStaffs().subscribe((response)=>{
        (response as any[]).forEach(element => {
          switch (element.jobType) {
            case "Teacher":
              element=element as Teacher;
              break;
            case "Admin":
              element=element as Admin;
              break;
            case "Support":
              element=element as Support;
              break;
            default:
              break;
          }
          this.staffs.push(element);
      });
      //  
    });
  }

}
