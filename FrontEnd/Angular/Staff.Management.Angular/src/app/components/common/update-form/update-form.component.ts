import { StaffService } from '../../../services/all-staffs.service';
import { Component, Input, OnInit, Output,EventEmitter } from '@angular/core';
import { observable } from 'rxjs';

@Component({
  selector: 'app-update-form',
  templateUrl: './update-form.component.html',
  styleUrls: ['./update-form.component.css']
})


export class UpdateFormComponent implements OnInit {

  @Input() staffsList:any;
  @Input() isAddForm:any; 
  @Input() staffID:any={staffID:-1};
  _staff={staffID:-1, name:"", gender:1,age:18, dailyWage:0, jobType:"",classTeacher:"",subject:"",section:"",lab:""};
  @Output() activePopUpForm= new EventEmitter();
  @Output() staffs=new EventEmitter();

  staff: any;
  jobType:any;
  constructor(private service: StaffService) { 
    
  }

  ngOnInit(): void {
    if (this.staffID>0) {
      this.service.GetAStaff(this.staffID).subscribe((response)=>{
        this._staff=response as any;      
      });
    }
    else{
      this.staff=this._staff; 
    }
    
   
  }
  ngOnChanges(){
    
  }
  UpdateStaff(){
    
    // console.log(this.staffsList, this._staff.staffID);
    this.service.UpdateStaff(this._staff).subscribe((response)=>{

      for (let index = 0; index < this.staffsList.length; index++) {
        if(this.staffsList[index].staffID==this._staff.staffID){
          this.staffsList[index]=this._staff;
          break;
        }
        
      }
      this.staffs.emit(this.staffsList);
      this.popUpClose();
      
    }) 
  }
  AddStaff(){
    //switching by job type
    var __staff:any={};
    __staff["name"]=this._staff.name;
    __staff["age"]=this._staff.age;
    __staff["gender"] =Number(this._staff.gender) ;
    __staff["dailyWage"]=this._staff.dailyWage;
    __staff["jobType"]=this._staff.jobType;
    switch (__staff.jobType) {
      case "Teacher":
        __staff["subject"]=this._staff.subject;
        __staff["classTeacher"]=this._staff.classTeacher;
        break;
      case "Admin":
        __staff["section"]=this._staff.section;
        break;
      case "Support":
        __staff["lab"]=this._staff.lab;
        break;
      default:
        break;
    }
    console.log(this._staff,__staff);
     this.service.AddStaff(__staff).subscribe((res)=>
        this.service.GetStaffs().subscribe((response: any)=>{
         this.staffs.emit(response);
         debugger;
        this._staff={staffID:-1, name:"", gender:1,age:18, dailyWage:0, jobType:"",classTeacher:"",subject:"",section:"",lab:""};
        }));
     
    // this.staffsList.push(__staff);
    this.popUpClose();
  }
  popUpClose(){
    this.activePopUpForm.emit(false);
    this.staff=null;
  }

}
