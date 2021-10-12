import { Gender } from 'src/app/services/all-staffs.service';
import { IStaff } from './Base/IStaff.model';
export class  Teacher  extends IStaff{
    staffID!: number;
    name!: string;
    age !: number;
    dailyWage !: number;
    gender !: Gender;
    jobType !: string;
    
    classTeacher: string ="";
    subject:string="";   
}