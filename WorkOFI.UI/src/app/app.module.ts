import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { TaskDetailComponent } from './components/task-detail/task-detail.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

import { AppRoutingModule } from './app-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AddTaskDialogComponent } from './components/add-task-dialog/add-task-dialog.component';
import { FormsModule } from '@angular/forms';
import { LogTimeDialogComponent } from './components/log-time/log-time.component';

@NgModule({
  declarations: [
    AppComponent,
    TaskDetailComponent,
    NavMenuComponent,
    DashboardComponent,
    AddTaskDialogComponent,
    LogTimeDialogComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
