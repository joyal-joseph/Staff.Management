import { Gender } from 'src/app/services/all-staffs.service';
import { IStaff } from './Base/IStaff.model';
export class Admin extends IStaff{
    staffID!: number;
    name!: string;
    age !: number;
    dailyWage !: number;
    gender !: Gender;
    jobType !: string;
    
    
    section: string ="";
}