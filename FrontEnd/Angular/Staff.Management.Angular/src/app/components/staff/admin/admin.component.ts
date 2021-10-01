import { Component, OnInit } from '@angular/core';
import { StaffService } from '../staff.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  title="List of Admin staffs"; 
  staffs:any[]=[];
  constructor(private service: StaffService) { 
    service.getStaffs().subscribe((response)=>{
    
    (response as any[]).forEach(element => {
      if(element.jobType=="Admin"){
        this.staffs.push(element);
      }
    });
  });
  }

  ngOnInit(): void {
  }

}
