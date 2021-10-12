
//import { Teacher } from 'src/app/Models/Teacher.model';
import { BaseComponent } from './../Base/base.class';
import { Component, OnInit } from '@angular/core';
import { Gender, StaffService } from '../../../services/all-staffs.service';
import { Teacher } from 'src/app/Models/Teacher.model';

@Component({
  selector: 'app-teacher',
  templateUrl: './teacher.component.html',
  styleUrls: ['./teacher.component.css']
})
export class TeacherComponent extends BaseComponent implements OnInit {

  title="List of all staffs";
  
  constructor(private _service: StaffService) { 
    super(_service);
  }

  ngOnInit(): void {
    this.service.GetStaffs().subscribe((response)=>{
      (response as any[]).forEach(element => {
        if(element.jobType=="Teacher"){
          //let ele =element as Teacher;
          element=element as Teacher;
          this.staffs.push(element);
        }
      });
    });
  }
}
