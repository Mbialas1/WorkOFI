import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddTaskDialogComponent } from '../add-task-dialog/add-task-dialog.component';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  
  constructor(private modalService: NgbModal){}

  openAddTaskModel(){
    const modalRef = this.modalService.open(AddTaskDialogComponent);
    modalRef.result.then((task) => {
      // Tutaj otrzymujesz zadanie po zamknięciu modalu z przyciskiem OK
    }).catch(() => {
      // Obsłuż sytuację, gdy modal został zamknięty w inny sposób (np. przycisk "Anuluj" lub kliknięcie poza oknem modalnym)
    });
  }
}
