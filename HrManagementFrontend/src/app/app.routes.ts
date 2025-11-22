import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: '/employees', pathMatch: 'full' },
  { 
    path: 'employees', 
    loadChildren: () => import('./employee/employee-module').then(m => m.EmployeeModule)
  }
];
