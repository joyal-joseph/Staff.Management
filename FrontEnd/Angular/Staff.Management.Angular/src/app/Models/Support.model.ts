import { Gender } from 'src/app/services/all-staffs.service';
import { IStaff } from './Base/IStaff.model';
export class Support implements IStaff{
    staffID: number | undefined;
    name: string | undefined;
    age: number | undefined;
    dailyWage: number | undefined;
    gender: Gender | undefined;
    jobType: string | undefined;
    
    lab: string =""; 
}