import { Gender } from 'src/app/services/all-staffs.service';
import { IStaff } from './Base/IStaff.model';
export class Teacher implements IStaff{
    staffID: number | undefined;
    name: string | undefined;
    age: number | undefined;
    dailyWage: number | undefined;
    gender: Gender | undefined;
    jobType: string | undefined;
    
    classTeacher: string ="";
    subject:string="";   
}