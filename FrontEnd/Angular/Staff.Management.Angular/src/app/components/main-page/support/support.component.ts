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
  paginationStartIndex:number=0;
  paginationEndIndex: number=9; 
  staffs:any[]=[];
  deleteList: number[]=[];
  debugElement:string="XYZ";
  RouterModule: any;
  updatingStaff: any;
  constructor(private service: StaffService) { 
    service.GetStaffs().subscribe((response)=>{
    
    (response as any[]).forEach(element => {
      if(element.jobType=="Support"){
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
  DeleteAStaff(StaffID: number){
    this.service.DeleteStaff(StaffID).subscribe(()=>{
      this.service.GetStaffs().subscribe((response)=>{
        this.staffs=response as any;
      })
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

UpdateStaff(staff: object){
  this.service.UpdateStaff(staff).subscribe(()=>{
    this.service.GetStaffs().subscribe((response)=>{
      this.staffs=response as any;
    })
  })
  this.updatingStaff=staff;
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
    console.log(staffID);
   })
  })
   this.service.GetStaffs().subscribe((response) =>{
      this.staffs=response as any;
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
