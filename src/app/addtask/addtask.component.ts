import { Component, OnInit } from '@angular/core';

import { Http, Headers } from '@angular/http';

import { NgForm, FormBuilder, FormGroup, Validators, FormControl,FormArray } from '@angular/forms';

import { Router, ActivatedRoute } from '@angular/router';

import { TasksService } from '../tasks.service'
import {NgxSpinnerService} from 'ngx-spinner';
@Component({
  selector: 'app-addtask',
  templateUrl: './addtask.component.html',
  styleUrls: ['./addtask.component.css']
})
export class AddtaskComponent implements OnInit {
parentTasks : ITask[];
  taskForm: FormGroup;

    title: string = "Create";

    id: number = 0;

    errorMessage: any;




    constructor(private _fb: FormBuilder, 
        private _avRoute: ActivatedRoute,
        private spinnerService :NgxSpinnerService,
        private _tasksService: TasksService, 
        private _router: Router) {
        if (this._avRoute.snapshot.params["id"]) {

            this.id = this._avRoute.snapshot.params["id"];

        }


		this.taskForm = this._fb.group({

            task_ID : 0,
            
            taskName: ['', [Validators.required]],

            priority: ['', [Validators.required]],

            parent_ID:[''],

            startDate: ['', [Validators.required]],

            endDate: ['', [Validators.required]]

        })

    }



    ngOnInit() {

        this._tasksService.getParentTasks()
      .subscribe(tasks => this.parentTasks = tasks);

        if (this.id > 0) {

            this.title = "Edit";
            this._tasksService.getTaskByID(this.id)

                .subscribe(resp => this.taskForm.setValue(resp)
                    , error => this.errorMessage = error);

        }
    }

    save() {

        if (!this.taskForm.valid) {

            return;

        }
        if (this.title == "Create") {
            this.spinnerService.show();
            this._tasksService.saveTask(this.taskForm.value)

                .subscribe((data) => {
                    this.spinnerService.hide();
                    this._router.navigate(['/view-tasks']);

                }, error => this.errorMessage = error)

        }

        else if (this.title == "Edit") {
        //    this.spinnerService.show();
        //     this._tasksService.(this.id, this.taskForm.value)

        //         .subscribe((data) => {
        //             this._router.navigate(['/view-tasks']);

        //         }, error => this.errorMessage = error)

        }

    }

    reset(){
        this.taskForm.reset();
    }

    cancel() {

       this._router.navigate(['/view-tasks']);

    }
    

    get taskName() { return this.taskForm.get('taskName'); }

    get priority() { return this.taskForm.get('priority'); }

    get parent_ID() { return this.taskForm.get('parent_ID'); }

    get startDate() { return this.taskForm.get('startDate'); }

    get endDate() { return this.taskForm.get('endDate'); }
}



export interface ITask {
  task_ID: number;
  parent_ID: number;
  taskName: string;
  parentTaskName: string;
  startDate: string,
  endDate: string,
  priority: number,
  isFinished: boolean
}
