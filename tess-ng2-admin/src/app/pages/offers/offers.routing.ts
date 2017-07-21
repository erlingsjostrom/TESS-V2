import { Routes, RouterModule }  from '@angular/router';
import { EditComponent } from './edit/edit.component';
import { AllOffersComponent } from './all/all-offers.component';
import { Offers } from './offers.component';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: Offers,
    children: [
      { path: '', component: AllOffersComponent },
      { path: 'all', component: AllOffersComponent },
      { path: 'edit', redirectTo: '' },
      { path: 'edit/:id', component: EditComponent },
    ]
  }
];

export const routing = RouterModule.forChild(routes);
