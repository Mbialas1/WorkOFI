export class StatusTaskDTO{
    taskId : number;
    statusTaskId : number;

    constructor(_idTask : number, _statusTask : number){
        this.taskId = _idTask;
        this.statusTaskId = _statusTask;
    }
}