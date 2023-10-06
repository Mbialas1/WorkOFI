import { Component } from '@angular/core';
import { ApiService } from 'src/app/api.service';
import { Task } from 'src/app/models/task.model';
import { User } from 'src/app/models/user.model';
import { UserService } from 'src/app/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  tasks: Task[] = [];
  users: User[] = [];
  selectedUser? : User;

  constructor(private userService: UserService, private taskService: ApiService){}

  defaultView() : void{
    this.tasks = [
      { id: 6, name: 'Zadanie 1', description: 'Opis zadania 1' },
      { id: 6, name: 'Zadanie 2', description: 'Opis zadania 2' },
    ];
  }

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe(data => {
      this.users = data;
      if(this.users.length > 0) {
        this.selectedUser = this.users[0];
        console.log(this.selectedUser);
        this.refreshTasksForSelectedUser();
      }
      else{
      this.defaultView();}
    });
  }

  onUserChanged() {
    this.refreshTasksForSelectedUser();
  }

  refreshTasksForSelectedUser() {
    if(this.selectedUser) {
      this.taskService.loadDashboardByUserId(this.selectedUser.id).subscribe(data => {
        this.tasks = data;
      });
    }
  }

}
