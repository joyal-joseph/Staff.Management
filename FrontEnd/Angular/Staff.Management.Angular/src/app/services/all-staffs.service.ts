
import { ErrorHandler, Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';

import { Observable, Subject, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
export enum Gender  { 
    Male , 
    Female ,
    Other 
  }

@Injectable()
export class StaffService{
    constructor(private http: HttpClient){}
    
    
    GetStaffs() {
        // //API end point to view all staff
        return this.http.get('http://staffmanagement.dev.com/api/Staff')
        // // .subscribe(response =>{
        // //     console.log(response.json());
        // // });
        // //return [];//return values should be in Staff object type
     
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
    DebugFunction(){
        
    }
}