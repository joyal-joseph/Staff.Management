import { StaffService } from '../../../services/all-staffs.service';
import { Component, Input, OnInit, Output,EventEmitter } from '@angular/core';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  @Output() staffs= new EventEmitter();
  @Input() deleteList:any;
  isAddForm=false;
  activePopUpForm=false;
  constructor(private service : StaffService) { }

  ngOnInit(): void {
  }
  AddStaff(){
    this.isAddForm=!this.isAddForm;
  }
  ngOnChanges(){
    console.log(this.deleteList);
  }//reference
  ActivePopUpFormChange(event: any){
    this.isAddForm=event
  }
  DataAfterAdd(event: any){
  }
  
}
