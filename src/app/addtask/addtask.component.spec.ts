import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddtaskComponent } from './addtask.component';
import { TasksService } from '../tasks.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AppModule } from '../app.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { APP_BASE_HREF } from '@angular/common';

describe('AddtaskComponent', () => {
  let component: AddtaskComponent;
  let fixture: ComponentFixture<AddtaskComponent>;
  let tasksService : TasksService;
  let spinnerService : NgxSpinnerService;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations : [AddtaskComponent],
      providers: [TasksService,NgxSpinnerService,{provide: APP_BASE_HREF, useValue : '/' }],
      imports : [BrowserModule,FormsModule,ReactiveFormsModule,HttpModule,RouterModule.forRoot([])]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    
    tasksService = TestBed.get(TasksService);
    fixture = TestBed.createComponent(AddtaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create addTaskComponent', () => {
    expect(component).toBeTruthy();
  });

 
  it('should call save method of addTaskComponent', () => {
    spyOn(tasksService, 'saveTask').and.callThrough();
    component.save();
    fixture.whenStable().then(() => {   
      expect(tasksService.saveTask).toHaveBeenCalled(); 
    });
  });

  it('should call reset method of addTaskComponent', () => {
    
    fixture.whenStable().then(() => {   
      expect(component.reset).toHaveBeenCalled(); 
    });
  });

  it('should call cancel method of addTaskComponent', () => {
    
    fixture.whenStable().then(() => {   
      expect(component.cancel).toHaveBeenCalled(); 
    });
  });
});
