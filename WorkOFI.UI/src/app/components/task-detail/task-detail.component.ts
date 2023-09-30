import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/api.service';
import { Task } from 'src/app/models/task.model';

@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.css']
})
export class TaskDetailComponent implements OnInit {

  constructor(private apiService: ApiService) { }

  task?: Task;

  ngOnInit(): void {
    const taksId = 6;
    this.apiService.getTaskById(taksId).subscribe(data => {
      this.task = data;
      console.log(this.task);
    })
  }

}