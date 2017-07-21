import { Routes, RouterModule }  from '@angular/router';
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
    ]
  }
];

export const routing = RouterModule.forChild(routes);
