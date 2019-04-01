import { TestBed, async } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { NgxSpinnerService, NgxSpinnerModule } from 'ngx-spinner';
import { TasksService } from './tasks.service';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { APP_BASE_HREF } from '@angular/common';
import { TasksearchPipe } from './tasksearch.pipe';
import { AppModule } from './app.module';

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppComponent ,TasksearchPipe],
      providers: [TasksService,NgxSpinnerService,{provide: APP_BASE_HREF, useValue : '/' }],
      imports : [NgxSpinnerModule,BrowserModule,FormsModule,ReactiveFormsModule,HttpModule,RouterModule.forRoot([])]
  
   
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });

  // it(`should have as title 'taskmanager'`, () => {
  //   const fixture = TestBed.createComponent(AppComponent);
  //   const app = fixture.debugElement.componentInstance;
  //   expect(app.title).toEqual('taskmanager');
  // });

  // it('should render title in a h1 tag', () => {
  //   const fixture = TestBed.createComponent(AppComponent);
  //   fixture.detectChanges();
  //   const compiled = fixture.debugElement.nativeElement;
  //   expect(compiled.querySelector('h1').textContent).toContain('Welcome to taskmanager!');
  // });
});
