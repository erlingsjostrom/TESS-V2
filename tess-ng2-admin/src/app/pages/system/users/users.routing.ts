import { TestComponent } from './test/test.component';
import { AllUsersComponent } from './all/all-users.component';
import { EditComponent } from './edit/edit.component';
import { Routes, RouterModule }  from '@angular/router';
import { Users } from './users.component';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: Users,
    children: [
      { path: '', component: AllUsersComponent },
      { path: 'all', component: AllUsersComponent },
      { path: 'edit', redirectTo: '' },
      { path: 'edit/:id', component: EditComponent },
      { path: 'test', component: TestComponent }
    ]
  }
];

export const routing = RouterModule.forChild(routes);
