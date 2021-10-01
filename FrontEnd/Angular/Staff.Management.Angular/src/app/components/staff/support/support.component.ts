import { Component, OnInit } from '@angular/core';
import { StaffService } from '../staff.service';

@Component({
  selector: 'app-support',
  templateUrl: './support.component.html',
  styleUrls: ['./support.component.css']
})
export class SupportComponent implements OnInit {

  
  title="List of Admin staffs"; 
  staffs:any[]=[];
  constructor(private service: StaffService) { 
    service.getStaffs().subscribe((response)=>{
    
    (response as any[]).forEach(element => {
      if(element.jobType=="Support"){
        this.staffs.push(element);
      }
      console.log(this.staffs);
    });
  });
  }

  ngOnInit(): void {
  }

}
