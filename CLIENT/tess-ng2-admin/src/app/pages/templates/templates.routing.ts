import { Routes, RouterModule }  from '@angular/router';
import { EditComponent } from './edit/edit.component';
import { AllComponent } from './all/all.component';
import { Templates } from './templates.component';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: Templates,
    children: [
      { path: '', component: AllComponent },
      { path: 'all', component: AllComponent },
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
