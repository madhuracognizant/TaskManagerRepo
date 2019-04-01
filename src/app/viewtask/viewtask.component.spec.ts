import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewtaskComponent } from './viewtask.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { TasksService } from '../tasks.service';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { TasksearchPipe } from '../tasksearch.pipe';
import { APP_BASE_HREF } from '@angular/common';

describe('ViewtaskComponent', () => {
  let component: ViewtaskComponent;
  let fixture: ComponentFixture<ViewtaskComponent>;
  let tasksService : TasksService;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewtaskComponent ,TasksearchPipe],
      providers: [TasksService,NgxSpinnerService,{provide: APP_BASE_HREF, useValue : '/' }],
      imports : [BrowserModule,FormsModule,ReactiveFormsModule,HttpModule,RouterModule.forRoot([])]
  
    })
    .compileComponents();
  }));

  beforeEach(() => {
    tasksService = TestBed.get(TasksService);
    fixture = TestBed.createComponent(ViewtaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call getTasks method of viewTaskComponent', () => {
    spyOn(tasksService, 'getTasks').and.callThrough();
    component.getTasks();
    fixture.whenStable().then(() => {   
      expect(tasksService.getTasks).toHaveBeenCalled(); 
    });
  });

});
