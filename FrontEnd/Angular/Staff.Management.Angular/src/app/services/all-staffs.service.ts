
import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';

export enum Gender  { 
    Male , 
    Female ,
    Other 
  }

@Injectable()
export class StaffService{

    StaffList:any;
    constructor(private http: HttpClient){}

    ngOnInit(){
       
    }  

    GetStaffs() {
        return this.http.get('http://staffmanagement.dev.com/api/Staff')      
    }

    GetAStaff(id: number){
        return this.http.get('http://staffmanagement.dev.com/api/Staff/'+id)
    }

    DeleteStaff(staffID: number){
        let deleteAPI = 'http://staffmanagement.dev.com/api/Staff/'+staffID;
        
         return this.http.delete(deleteAPI);        
    }
    
    UpdateStaff(staff: any){
        return this.http.put('http://staffmanagement.dev.com/api/Staff/'+staff.staffID, staff, {headers:{'Content-Type':'application/json'}});
    }

    AddStaff(staff: object){
        return this.http.post('http://staffmanagement.dev.com/api/Staff', staff);
    }
}