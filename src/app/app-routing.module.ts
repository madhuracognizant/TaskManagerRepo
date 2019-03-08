import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AddtaskComponent} from './addtask/addtask.component';
import {ViewtaskComponent} from './viewtask/viewtask.component';

const routes: Routes = [
  { path: '', redirectTo:'/view-tasks',pathMatch:'full' },
  { path: 'add-task', component: AddtaskComponent },
  { path: 'edit-task/:id', component: AddtaskComponent },
  { path: 'view-tasks', component: ViewtaskComponent },
  { path: '**', redirectTo:'view-tasks' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
