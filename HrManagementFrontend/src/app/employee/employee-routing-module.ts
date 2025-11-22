import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeList } from './employee-list/employee-list';
import { EmployeeDetail } from './employee-detail/employee-detail';
import { EmployeeForm } from './employee-form/employee-form';

const routes: Routes = [
  { path: '', component: EmployeeList },
  { path: 'new', component: EmployeeForm },
  { path: ':id', component: EmployeeDetail },
  { path: ':id/edit', component: EmployeeForm }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }
