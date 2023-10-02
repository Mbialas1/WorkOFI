import { Component } from '@angular/core';
import { Task } from 'src/app/models/task.model';

@Component({
    selector: 'app-add-task-dialog',
    templateUrl: './add-task-dialog.component.html',
    styleUrls: ['./add-task-dialog.component.css']
})
export class AddTaskDialogComponent {

    task : Task = {
        id: 0,
        name: '',
        description: ''
    };

    constructor() { }

    addTask() {
        console.log(this.task);
        this.closeDialog();
    }

    closeDialog() {
    }
}
