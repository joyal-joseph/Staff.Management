import { StaffService } from '../../../services/all-staffs.service';
import { Component, Input, OnInit, Output,EventEmitter } from '@angular/core';

@Component({
  selector: 'app-update-form',
  templateUrl: './update-form.component.html',
  styleUrls: ['./update-form.component.css']
})


export class UpdateFormComponent implements OnInit {

  
  @Input() staff:any={staffID:-1};
  @Input() isAddForm:any=false;
  @Output() activePopUpForm= new EventEmitter();
  @Output() staffs=new EventEmitter();
  constructor(private service: StaffService) { }

  ngOnInit(): void {
  }
  ngOnChanges(){
    console.log(this.staff);
  }
  UpdateStaff(){
    this.service.UpdateStaff(this.staff).subscribe((response)=>
      this.service.GetStaffs().subscribe((response: any)=>{
        this.staffs.emit(response) }))
  }
  popUpClose(){
    this.activePopUpForm.emit(false);
    this.staff=null;
  }

}
