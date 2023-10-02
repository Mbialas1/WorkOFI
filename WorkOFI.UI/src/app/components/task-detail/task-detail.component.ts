import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/api.service';
import { Task } from 'src/app/models/task.model';
import { Log } from 'src/app/models/log.model';
import { Comment } from 'src/app/models/comment.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.css']
})
export class TaskDetailComponent implements OnInit {

  constructor(private apiService: ApiService, private route: ActivatedRoute) { }

  task?: Task;
  timeLogs? : Log[];
  comments? : Comment[];

  ngOnInit(): void {
    const idValue = this.route.snapshot.paramMap.get('id');
    const taskId = idValue ? +idValue : 0;
    if(taskId > 0){
    this.apiService.getTaskById(taskId).subscribe(data => {
      this.task = data;
    })
  }
  }
}