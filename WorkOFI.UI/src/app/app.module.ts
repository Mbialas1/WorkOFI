import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { TaskDetailComponent } from './components/task-detail/task-detail.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [
    AppComponent,
    TaskDetailComponent,
    NavMenuComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
