import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogTimeComponent } from './log-time.component';

describe('LogTimeComponent', () => {
  let component: LogTimeComponent;
  let fixture: ComponentFixture<LogTimeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LogTimeComponent]
    });
    fixture = TestBed.createComponent(LogTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
