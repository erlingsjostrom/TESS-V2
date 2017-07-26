import { ViewComponent } from './view/view.component';
import { Routes, RouterModule }  from '@angular/router';
import { EditComponent } from './edit/edit.component';
import { AllComponent } from './all/all.component';
import { Offers } from './offers.component';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: Offers,
    children: [
      { path: '', component: AllComponent },
      { path: 'all', component: AllComponent },
      { path: 'view', redirectTo: ''},
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
      { 
        path: 'view/:id', 
        data: {
          breadcrumb: 'View'
        },
        component: ViewComponent 
      },
    ]
  }
];

export const routing = RouterModule.forChild(routes);
