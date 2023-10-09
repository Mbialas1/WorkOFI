import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { LogTimeModel } from 'src/app/models/logTime.model';
@Component({
  selector: 'app-log-time',
  templateUrl: './log-time.component.html'
})
export class LogTimeDialogComponent {
  inputText? : string;
  taskId? : number;

  constructor(public activeModal: NgbActiveModal) {}

  logTime() {
    if(!this.inputText || this.inputText.length === 0){
      console.error("empty text input");
      return;
    }

    

    this.activeModal.close();
  }
}
