import { Component } from '@angular/core';
import { ApiService } from 'src/app/api.service';
import { Task } from 'src/app/models/task.model';
import { User } from 'src/app/models/user.model';
import { UserService } from 'src/app/user.service';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  userControl = new FormControl();
  tasks: Task[] = [];
  users: User[] = [];
  selectedUser? : User;
  page: number = 1;
  pageSize: number = 10;
  isAllTasksLoaded: boolean = false;
  filteredUsers = this.userControl.valueChanges.pipe(
    debounceTime(300), 
    distinctUntilChanged(),
    filter(query => query && query.length > 2 && /^[a-zA-Z\s]+$/.test(query)),
    switchMap(query => this.userService.searchUsers(query.toLowerCase())) 
  );
  

  constructor(private userService: UserService, private taskService: ApiService, private router: Router){}

  defaultView() : void{
    this.tasks = [
      { id: 6, name: 'Zadanie 1', description: 'Opis zadania 1' },
      { id: 6, name: 'Zadanie 2', description: 'Opis zadania 2' },
    ];
  }

  navigateToTaskDetails(taskId: number) {
    this.router.navigate(['/task', taskId]);
 }

  ngOnInit(): void {
      this.defaultView();
  }

  displayFn(user?: User): string {
    return user ? user.firstName + ' ' + user.lastName : '';
  }

  onUserChanged(selUser: User) {
    this.selectedUser = selUser;
    console.log('User change');
    this.page = 1;
    this.pageSize = 10;
    this.isAllTasksLoaded = false;
    this.tasks = [];
    this.refreshTasksForSelectedUser();
  }

  refreshTasksForSelectedUser() {
    if(this.selectedUser && !this.isAllTasksLoaded) {
      this.taskService.loadDashboardByUserId(this.selectedUser.id, this.page, this.pageSize).subscribe(data => {
        if(data.length < this.pageSize){
          this.isAllTasksLoaded = true;
        }
        this.tasks = [...this.tasks, ...data];
        this.page++;
      });
    }
  }

}
