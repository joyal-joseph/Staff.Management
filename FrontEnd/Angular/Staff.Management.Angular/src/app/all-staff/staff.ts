enum Genders{Male, Female, Other};

export abstract class Staff {
    
    public StaffID!: number; 
    public Name!: string;
    public Age!: number;
    public DailyWage!: number;
    public Gender!: Genders;
    
}
