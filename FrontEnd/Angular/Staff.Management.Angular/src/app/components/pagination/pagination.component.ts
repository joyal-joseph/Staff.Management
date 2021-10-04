import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { StaffService } from '../staffs/all-staffs/all-staffs.service';
// import { StaffsModule } from '../staffs/staffs.module';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit {
  @Input() title=""; 
  @Input() staffs!:any[];
  @Input() numberOfStaffs=0;

  @Output() startAndEndIndeces= new EventEmitter();
  
  noOfPages: number= 1;
  pages: number[]=[];
  
  
  
  constructor() { };

  ngOnInit(): void {
    ;
  }
  ngOnChanges(){
    this.noOfPages=Math.ceil(this.numberOfStaffs/10);
    this.pages = Array.from(Array(this.noOfPages), (x, i) => i + 1)
  }
  
  ChangePage(currentPageNumber: number) {
    let startIndex= (currentPageNumber-1)*10;
    let endIndex= (currentPageNumber)*10-1;
    this.startAndEndIndeces.emit({startIndex,endIndex})
  }
}
