import { Component } from '@angular/core';
import { Task } from 'src/app/models/task.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  tasks: Task[] = [];

  ngOnInit(): void {
    this.tasks = [
      { id: 6, name: 'Zadanie 1', description: 'Opis zadania 1' },
      { id: 6, name: 'Zadanie 2', description: 'Opis zadania 2' },
    ];
  }
}
