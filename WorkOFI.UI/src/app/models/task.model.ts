export interface Task {

    id: number; 
    name: string;
    description: string;
    tottalRemaining?: Date;
    lastEditTime?: Date;
    nameOfUser?: string;
    createdDate?: Date;
    taskStatus?: string; // TODO Change for non-nullable
    totalRemaing? : Date;
}
