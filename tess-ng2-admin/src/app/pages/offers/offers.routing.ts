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
      { path: 'edit', redirectTo: '' },
      { path: 'edit/:id', component: EditComponent },
      { path: 'create', component: EditComponent },
      { path: 'view', redirectTo: ''},
      { path: 'view/:id', component: ViewComponent },
    ]
  }
];

export const routing = RouterModule.forChild(routes);
