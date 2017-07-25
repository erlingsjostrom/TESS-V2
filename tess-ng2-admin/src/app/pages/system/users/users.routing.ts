import { AllComponent } from './all/all.component';
import { EditComponent } from './edit/edit.component';
import { Routes, RouterModule }  from '@angular/router';
import { Users } from './users.component';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: 'users',
    data: {
      breadcrumb: 'Users',
    }, 
    component: Users,
    children: [
      { path: '', component: AllComponent },
      { path: 'all', redirectTo: '' },
      { path: 'edit', redirectTo: '' },
      { 
        path: 'edit/:id', 
        data: {
          breadcrumb: 'Edit',
        }, 
        component: EditComponent 
      },
      { 
        path: 'create', 
        data: {
          breadcrumb: 'Create',
        }, 
        component: EditComponent 
      },
    ]
  }
];

export const routing = RouterModule.forChild(routes);
