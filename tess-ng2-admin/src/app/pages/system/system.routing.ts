import { Routes, RouterModule }  from '@angular/router';

import { System } from './system.component';
import { Users } from './components/users/users.component';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: System,
    children: [
      { path: 'users', component: Users }
    ]
  }
];

export const routing = RouterModule.forChild(routes);
