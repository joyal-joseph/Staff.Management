
import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
@Injectable()
export class StaffService{
    constructor(private http: HttpClient){}
    
    getStaffs(){
        //API end point to view all staff
        return this.http.get('http://staffmanagement.dev.com/api/Staff')
        // .subscribe(response =>{
        //     console.log(response.json());
        // });
        //return [];//return values should be in Staff object type
    }
}