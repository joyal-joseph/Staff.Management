import { Gender } from '../../services/all-staffs.service';
export class IStaff{
    staffID: number =0 ;
    name: string ="";
    age: number | undefined ;
    dailyWage: number | undefined ;
    gender: Gender | undefined ;
    jobType: string | undefined ;

    classTeacher:string | undefined;
    subject: string | undefined;
    lab: string | undefined;
    section: string | undefined;

    // staffID: number ;
    // name: string ;
    // age: number ;
    // dailyWage: number ;
    // gender: Gender ;
    // jobType: string ;
}
