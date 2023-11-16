import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeLogTableComponent } from './time-log-table.component';

describe('TimeLogTableComponent', () => {
  let component: TimeLogTableComponent;
  let fixture: ComponentFixture<TimeLogTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TimeLogTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TimeLogTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
