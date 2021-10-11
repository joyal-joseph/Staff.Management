
import { ErrorHandler, Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';

import { Observable, of, Subject, throwError } from 'rxjs';
import { catchError, retry, switchMap } from 'rxjs/operators';
export enum Gender  { 
    Male , 
    Female ,
    Other 
  }

@Injectable()
export class StaffService{
    // Observable: any observableStaffs = of(this.GetStaffs());

    StaffList:any;

    // obs= new Observable((observer)=>{console.log("debug");
    // observer.next();
    // });
    constructor(private http: HttpClient){}

    ngOnInit(){
        //const sqnc = new Observable(this.GetStaffs)
       
    }
    
    
    GetStaffs() {
        // //API end point to view all staff
        return this.http.get('http://staffmanagement.dev.com/api/Staff')
        // .pipe(switchMap((result)=>{
            
        //     let allStaffs: Staff;
        //     allStaffs = <Staff[]>result;
        //     return (staffClass[])
        // })

        // // .subscribe(response =>{
        // //     console.log(response.json());
        // // });
        // //return [];//return values should be in Staff object type
     
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
    DebugFunction(){
        
    }
}