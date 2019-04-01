import { Injectable } from '@angular/core';
import { Http, Response} from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import { ITask } from './addtask/addtask.component';

@Injectable()
export class TasksService {
    private BASE_URL = 'http://localhost/TaskManagerAPI/api/Tasks/';
	//private BASE_URL = 'http://localhost:49483/api/tasks/';
    constructor(private _http: Http) { }

    
    getTasks(): Observable<ITask[]> {
        return this._http.get(this.BASE_URL)
            .map((response: Response) => <ITask[]>response.json())
            .do(data => console.log(JSON.stringify(data)));
    }

    getTaskByID(taskID : number): Observable<ITask> {
        return this._http.get(this.BASE_URL+taskID)
            .map((response: Response) => <ITask>response.json())
            .do(data => console.log(JSON.stringify(data)));
    }

    getParentTasks(): Observable<ITask[]> {
        return this._http.get(this.BASE_URL+"/ParentTasks")
            .map((response: Response) => <ITask[]>response.json())
            .do(data => console.log(JSON.stringify(data)));
    }
     
    getParentTaskByID(parent_id :number): Observable<ITask> {
        return this._http.get(this.BASE_URL+parent_id)
            .map((response: Response) => <ITask>response.json())
            .do(data => console.log(JSON.stringify(data)));
    }
     
    // editTask(taskID : number): Observable<ITask[]> {
    //     return this._http.put(this.BASE_URL,"")
    //         .map((response: Response) => <ITask[]>response.json())
    //         .do(data => console.log(JSON.stringify(data)));
    // }

    saveTask(task) {

        return this._http.post(this.BASE_URL + 'SaveTask', task)
            .map((response: Response) => response.json())
            .do(data => console.log(JSON.stringify(data)));
    } 

    endTask(taskID:number)
    {
        return this._http.delete(this.BASE_URL + 'EndTask/'+ taskID)
        .map((response: Response) => response.json())
        .do(data => console.log(JSON.stringify(data)));
    }
}
