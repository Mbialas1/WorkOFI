import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { LogTimeModel } from 'src/app/models/logTime.model';
import { ApiService } from 'src/app/api.service';

@Component({
  selector: 'app-log-time',
  templateUrl: './log-time.component.html'
})
export class LogTimeDialogComponent {
  inputText? : string;
  taskId? : number;

  constructor(public activeModal: NgbActiveModal, private taskService : ApiService) {}

  logTime() {
    if(!this.inputText || this.inputText.length === 0){
      console.error("empty text input");
      return;
    }

    if(!(this.taskId)){
      console.error('Not task id set');
      return;
    }

    let logModel = new LogTimeModel(this.inputText, this.taskId);
    this.taskService.logTimeToTask(logModel).subscribe({
      next: response => {
          console.log('Send request for log to this task. Wait and refresh website for check changes.', response);
      },
      error: error => {
          console.error('Something wrong from TaskAPI', error);
      }
  });
    this.activeModal.close();
  }
}
