import { Component, OnInit } from '@angular/core';
import { StaffService } from './all-staffs.service';

@Component({
  selector: 'app-all-staffs',
  templateUrl: './all-staffs.component.html',
  styleUrls: ['./all-staffs.component.css']
})
export class AllStaffsComponent implements OnInit {

  title="List of all staffs";
  paginationStartIndex:number=0;
  paginationEndIndex: number=9; 
  staffs:any[]=[];
  debugElement:string="XYZ";
  constructor(private service: StaffService ) { 
    service.getStaffs().subscribe((response)=>{console.log(response);
    this.staffs=response as any ;
  console.log(this.staffs)});
  }

  ngOnInit(): void {
  }
  StaffPrinting(event : any){
    this.paginationStartIndex=event.startIndex;
    this.paginationEndIndex=event.endIndex;
  }


}
