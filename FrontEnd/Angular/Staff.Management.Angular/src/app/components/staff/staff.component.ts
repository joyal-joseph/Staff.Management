import { StaffService } from './staff.service';
import { Component, OnInit } from '@angular/core';
import { Staff } from 'src/app/all-staff/staff';

@Component({
  selector: 'app-staff',
  templateUrl: './staff.component.html',
  styleUrls: ['./staff.component.css']
})
export class StaffComponent implements OnInit {
  title="List of all staffs"; 
  staffs:any[]=[];
  debugElement:string="XYZ";
  constructor(private service: StaffService) { 
    service.getStaffs().subscribe((response)=>{console.log(response);
    this.staffs=response as any ;
  console.log(this.staffs)});
  }
  ngOnInit(): void {
  }

 
}
