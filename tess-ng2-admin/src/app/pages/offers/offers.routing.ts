import { Routes, RouterModule }  from '@angular/router';

import { Offers } from './offers.component';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: Offers,
    children: [
      { path: 'offers', loadChildren: '' }
    ]
  }
];

export const routing = RouterModule.forChild(routes);
