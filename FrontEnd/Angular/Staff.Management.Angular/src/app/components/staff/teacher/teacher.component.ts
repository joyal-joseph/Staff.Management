import { Component, OnInit } from '@angular/core';
import { StaffService } from '../staff.service';

@Component({
  selector: 'app-teacher',
  templateUrl: './teacher.component.html',
  styleUrls: ['./teacher.component.css']
})
export class TeacherComponent implements OnInit {

  title="List of Admin staffs"; 
  staffs:any[]=[];
  constructor(private service: StaffService) { 
    service.getStaffs().subscribe((response)=>{
    
    (response as any[]).forEach(element => {
      if(element.jobType=="Teacher"){
        this.staffs.push(element);
      }
    });
  });
  }

  ngOnInit(): void {
  }

}
