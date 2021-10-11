import { Component, OnInit } from '@angular/core';
import { Gender, StaffService } from '../../../services/all-staffs.service';

@Component({
  selector: 'app-support',
  templateUrl: './support.component.html',
  styleUrls: ['./support.component.css']
})
export class SupportComponent implements OnInit {

  
  allChecked=false;
  activePopUpForm=false;
  isAddForm=false;
  title="List of Support Staffs";
  activePage:number=1;
  paginationStartIndex:number=0;
  paginationEndIndex: number=9; 
  staffs:any[]=[];
  deleteList: number[]=[];
  RouterModule: any;
  updatingStaffID: any ;
  constructor(private service: StaffService) { 
    
  }

  ngOnInit(): void {
    this.service.GetStaffs().subscribe((response)=>{
      (response as any[]).forEach(element => {
        if(element.jobType=="Support"){
          this.staffs.push(element);
        }
      });
    });
  }
  StaffPrinting(event : any){
    this.paginationStartIndex=event.startIndex;
    this.paginationEndIndex=event.endIndex;
    this.activePage=event.activePage;
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
}

newStaffs(staffs:any){
  this.staffs=staffs;
  console.log(staffs)
}

UpdateStaff(staffID: number){
  this.updatingStaffID=staffID;
  this.activePopUpForm=true;
  this.isAddForm=false;
}

AddStaff(){
  this.activePopUpForm=true;
  this.isAddForm=true;
  this.updatingStaffID=-1;
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
