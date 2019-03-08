import { Pipe, PipeTransform } from '@angular/core';
import { ITask } from './addtask/addtask.component';


@Pipe({
  name: 'tasksearch'
})
export class TasksearchPipe implements PipeTransform {
  transform(items: ITask[], tasknameSearch: string, parentTaskNameSearch: string, 
    priorityFromSearch: string, priorityToSearch: string,
    startDateSearch: string,endDateSearch: string){
      if (items && items.length){
          return items.filter(item =>{
              if (tasknameSearch && item.taskName.toLowerCase().indexOf(tasknameSearch.toLowerCase()) === -1){
                  return false;
              }
              if (parentTaskNameSearch && item.parentTaskName.toLowerCase().indexOf(parentTaskNameSearch.toLowerCase()) === -1){
                  return false;
              }
              if (priorityFromSearch  && item.priority.toString().toLowerCase() < priorityFromSearch.toLowerCase()){
                  return false;
              }
              if (priorityToSearch  && item.priority.toString().toLowerCase() > priorityToSearch.toLowerCase()){
                return false;
              }
              if (startDateSearch && Date.parse(item.startDate.toLowerCase())  < Date.parse(startDateSearch.toLowerCase())){
                return false;
              }
              if (endDateSearch && Date.parse(item.endDate.toLowerCase())  >  Date.parse(endDateSearch.toLowerCase())){
                return false;
              }
              return true;
         })
      }
      else{
          return items;
      }
  }
}
