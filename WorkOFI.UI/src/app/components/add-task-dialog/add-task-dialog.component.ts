import { Component, OnDestroy } from '@angular/core';
import { Task } from 'src/app/models/task.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ApiService } from 'src/app/api.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

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

    private destroy$ = new Subject<void>();

    constructor(private activeMode: NgbActiveModal, private taskService: ApiService) { }

    addTask() {
        this.taskService.addTask(this.task)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
            next: response => {
                console.log('Add task done', response);
                this.closeDialog();
            },
            error: error => {
                console.log('Cannot add task', error);
            }
        });
    }

    closeDialog() {
        this.activeMode.dismiss();
    }

    ngOnDestroy(){
        this.destroy$.next();
        this.destroy$.complete();
    }
}
