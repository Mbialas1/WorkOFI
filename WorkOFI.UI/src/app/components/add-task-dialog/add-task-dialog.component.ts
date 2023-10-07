import { Component, OnDestroy } from '@angular/core';
import { AddingTask } from 'src/app/models/addingTask.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ApiService } from 'src/app/api.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { User } from 'src/app/models/user.model';
import { UserService } from 'src/app/user.service';

@Component({
    selector: 'app-add-task-dialog',
    templateUrl: './add-task-dialog.component.html',
    styleUrls: ['./add-task-dialog.component.css']
})
export class AddTaskDialogComponent {
    task : AddingTask = {
        id: 0,
        name: '',
        description: ''
    };

    private destroy$ = new Subject<void>();
    selectedUser? : User;
    users: User[] = [];

    constructor(private activeMode: NgbActiveModal, private taskService: ApiService, private userService: UserService) { }

    addingTask = false;

addTask() {
    this.addingTask = true;

    this.task.assignedUserId = this.selectedUser?.id;
    if(!this.task.assignedUserId){
        console.error('Cant set id of user!');
        this.addingTask = false;
        return;
    }
    
    this.taskService.addTask(this.task)
    .pipe(takeUntil(this.destroy$))
    .subscribe({
        next: response => {
            console.log('Add task done', response);
            this.closeDialog();
            this.addingTask = false;
        },
        error: error => {
            console.log('Cannot add task', error);
            this.addingTask = false;
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

    ngOnInit(){
        this.userService.getAllUsers().subscribe(data => {
            this.users = data;
            if(this.users.length > 0){
                this.selectedUser = this.users[0];
            }
        });
    }
}
