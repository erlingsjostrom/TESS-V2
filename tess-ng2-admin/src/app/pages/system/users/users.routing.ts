import { AllComponent } from './all/all.component';
import { EditComponent } from './edit/edit.component';
import { Routes, RouterModule }  from '@angular/router';
import { Users } from './users.component';



// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: Users,
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
