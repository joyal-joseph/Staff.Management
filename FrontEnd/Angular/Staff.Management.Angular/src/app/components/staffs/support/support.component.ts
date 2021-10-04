import { Component, OnInit } from '@angular/core';
import { StaffService } from '../all-staffs/all-staffs.service';

@Component({
  selector: 'app-support',
  templateUrl: './support.component.html',
  styleUrls: ['./support.component.css']
})
export class SupportComponent implements OnInit {

  
  title="List of Support staffs"; 
  paginationStartIndex:number=0;
  paginationEndIndex: number=9;
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
  StaffPrinting(event : any){
    this.paginationStartIndex=event.startIndex;
    this.paginationEndIndex=event.endIndex;
  }

}
