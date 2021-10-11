import { Gender } from '../../services/all-staffs.service';
export interface IStaff{
    staffID: number | undefined;
    name: string | undefined;
    age: number | undefined;
    dailyWage: number | undefined;
    gender: Gender | undefined;
    jobType: string | undefined;
}