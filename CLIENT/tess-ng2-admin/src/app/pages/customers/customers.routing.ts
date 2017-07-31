import { Routes, RouterModule }  from '@angular/router';
import { EditComponent } from './edit/edit.component';
import { AllComponent } from './all/all.component';
import { Customers } from './customers.component';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: Customers,
    children: [
      { path: '', component: AllComponent },
      { path: 'all', component: AllComponent },
      { path: 'edit', redirectTo: '' },
      { path: 'edit/:id', component: EditComponent },
      { path: 'create', component: EditComponent },
    ]
  }
];

export const routing = RouterModule.forChild(routes);
