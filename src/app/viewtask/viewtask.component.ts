import { Component, Inject, OnInit } from '@angular/core';
import 'rxjs/add/operator/map';
import { TasksService } from '../tasks.service';
import { ITask } from '../addtask/addtask.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-viewtask',
  templateUrl: './viewtask.component.html',
  styleUrls: ['./viewtask.component.css']
})
export class ViewtaskComponent implements OnInit {
    public tasks: ITask[] = [];
    public parentTask : ITask;
   
    constructor(private _router:Router,private _tasksService: TasksService) {
        
    }
    ngOnInit(): void {
        this.getTasks();
    }

    getTasks() {
      this._tasksService.getTasks()
      .subscribe(tasks => 
	  {
          this.tasks = tasks;
      })
      
    }  

    editTask(taskID : number)
    {
        this._router.navigate(['edit-task/'+taskID])
    }

    endTask(taskID:number){
        this._tasksService.endTask(taskID)
        .subscribe(tasks =>  tasks);
        this._router.navigate(['/view-tasks'])
    }
}
