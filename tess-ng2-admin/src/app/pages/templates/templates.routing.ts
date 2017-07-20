import { Routes, RouterModule }  from '@angular/router';

import { Templates } from './templates.component';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: Templates,
    children: [
      { path: 'templates', loadChildren: '' }
    ]
  }
];

export const routing = RouterModule.forChild(routes);
