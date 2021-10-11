
import { NgIf } from '@angular/common';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { isDelegatedFactoryMetadata } from '@angular/compiler/src/render3/r3_factory';
import { Component, OnInit, Output,EventEmitter } from '@angular/core';
import { from, Observable, of } from 'rxjs';
import { StaffService, Gender } from '../../../services/all-staffs.service';



@Component({
  selector: 'app-all-staffs',
  templateUrl: './all-staffs.component.html',
  styleUrls: ['./all-staffs.component.css']
})


export class AllStaffsComponent implements OnInit {

  

  staffs:any[]=[];
  //const myObservable = of(this.staffs);
  allChecked=false;
  activePopUpForm=false;
  isAddForm=false;
  title="List of all staffs";
  paginationStartIndex:number=0;
  paginationEndIndex: number=9; 
  
  deleteList: number[]=[];
  debugElement:string="XYZ";
  RouterModule: any;
  updatingStaffID: any;
  constructor(private service: StaffService ) { 
   
  }
  

  ngOnInit(): void {
    this.service.GetStaffs().subscribe((response)=>{
      this.staffs=response as any;  
    });

  }
  StaffPrinting(event : any){
    this.paginationStartIndex=event.startIndex;
    this.paginationEndIndex=event.endIndex;
  }
  DeleteAStaff(StaffID: number){
    
    this.service.DeleteStaff(StaffID).subscribe(()=>{
      this.staffs.splice(this.staffs.findIndex(eachStaff => eachStaff.staffID==StaffID)  ,1)
    }); 
      
  }

selectAllCheckBox(){

    if(!this.allChecked){
      this.deleteList=[];
      this.service.GetStaffs().subscribe((response : any)=>{
        response.forEach((element: { staffID: number; })  => {
          this.deleteList.push(element.staffID);
        });
      }
      )
      this.allChecked=!this.allChecked;
    }
    else{
      this.deleteList=[];
      this.allChecked=!this.allChecked;
    }
  
}
selectACheckBox(staffID:number){

  if (this.deleteList.indexOf(staffID) != -1) {
      this.deleteList.splice(this.deleteList.indexOf(staffID),1);
  } else {
    this.deleteList.push(staffID) ;
    
  }
  this.deleteList=[...this.deleteList]
  //this.staffs
}

newStaffs(staffs:any){
  this.staffs=staffs;
  console.log(staffs)
}

UpdateStaff(staffID: number){
  // this.service.UpdateStaff(staff).subscribe(()=>{
  //   this.service.GetStaffs().subscribe((response)=>{
  //     this.staffs=response as any;
  //   })
  // });
  this.updatingStaffID=staffID;
  this.activePopUpForm=true;
  this.isAddForm=false;
}

AddStaff(){
  // this.service.AddStaff(staff).subscribe(()=>{
  //   this.service.GetStaffs().subscribe((response)=>{
  //     this.staffs=response as any;
  //   })
  // })
  this.activePopUpForm=true;
  this.isAddForm=true;
  
}
ActivePopUpFormChange(event: any){
  this.activePopUpForm=event
}
DataAfterUpdate(event: any){
  this.staffs=event;
}

DeleteStaffs(){
    
  this.deleteList.forEach(async (staffID: number) => {
   await this.service.DeleteStaff(staffID).subscribe((response) =>{
    this.staffs.splice(this.staffs.findIndex(eachStaff => eachStaff.staffID==staffID)  ,1);
   })
  });

}

SortByName(){
  this.staffs.sort((a, b) => (a.name < b.name ? -1 : 1));
}
SortByID(){
  this.staffs.sort((a, b) => (a.staffID < b.staffID ? -1 : 1));
}
GenderFunction(gender: number){
  return Gender[gender];
}
}
