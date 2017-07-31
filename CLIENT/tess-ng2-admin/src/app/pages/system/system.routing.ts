import { Routes, RouterModule }  from '@angular/router';
import { System } from './system.component';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: System,
    children: [
      { path: '', redirectTo: "users" },
      { path: 'users', loadChildren: './users/users.module#UsersModule' }
    ]
  }
];

export const routing = RouterModule.forChild(routes);
