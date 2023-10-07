export interface Task {

    id: number; 
    name: string;
    description: string;
    loggedTime?: Date;
    lastEdited?: Date;
    assignedUser?: string;
    createdDate?: Date;
    taskStatus?: string; // TODO Change for non-nullable
    totalRemaing? : Date;
}
