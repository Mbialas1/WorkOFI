export class LogTimeModel{
    time : string;
    taskId : number;

    constructor(_time : string, _taskId : number){
        this.time = _time;
        this.taskId = _taskId;
    }
}