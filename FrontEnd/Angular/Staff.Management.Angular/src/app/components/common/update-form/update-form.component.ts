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
    this.service.GetAStaff(this.staffID).subscribe((response)=>{
      this.staff=response;      
    });
   
  }
  ngOnChanges(){
    
  }
  UpdateStaff(){
    // this.service.UpdateStaff(this.staff).subscribe((response)=>
    //   this.service.GetStaffs().subscribe((response: any)=>{
    //     this.staffs.emit(response) }));

    // this.service.UpdateStaff(this.staff).subscribe((response)=>{
    //   this.staffsList.find((i: any)=>{
    //   if(this.staff.staffID=== i.staffID){
    //     console.log(this.staff, i);
    //   }
    // });
    // });
    // this.service.GetAStaff(this.staffID).subscribe((response)=>{
    //   this.staff=response;
    //   console.log(this.staff,response);
    // }
    // )
    
    console.log(this.staffsList, this.staff.staffID);
    this.service.UpdateStaff(this.staff).subscribe((response)=>{

      for (let index = 0; index < this.staffsList.length; index++) {
        if(this.staffsList[index].staffID==this.staff.staffID){
          this.staffsList[index]=this.staff;
          break;
        }
        
      }
      this.staffs.emit(this.staffsList);
      this.activePopUpForm.emit(false);
      this.staff=null;
      
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
     this.service.AddStaff(__staff).subscribe((response)=>
        this.service.GetStaffs().subscribe((response: any)=>{
         this.staffs.emit(response);
        this._staff={staffID:-1, name:"", gender:1,age:18, dailyWage:0, jobType:"",classTeacher:"",subject:"",section:"",lab:""};
        }));
      
    this.activePopUpForm.emit(false);
    this.staff=null;
  }
  popUpClose(){
    this.activePopUpForm.emit(false);
    this.staff=null;
  }

}
