import { Component, OnInit } from '@angular/core';
import { StaffService } from '../all-staffs/all-staffs.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  title="List of Admin staffs";
  paginationStartIndex:number=0;
  paginationEndIndex: number=9; 
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
  StaffPrinting(event : any){
    this.paginationStartIndex=event.startIndex;
    this.paginationEndIndex=event.endIndex;
  }


}
