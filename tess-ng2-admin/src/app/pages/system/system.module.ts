import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../shared/users/user.service';
import { NgaModule } from '../../theme/nga.module';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { Users } from './components/users/users.component';
import { System } from './system.component';
import { routing } from './system.routing';

import { ArrayJoinRenderComponent } from './components/users/array-join-render.component';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgaModule,
    Ng2SmartTableModule,
    routing,
  ],
  entryComponents: [
    ArrayJoinRenderComponent,
  ],
  declarations: [
    System,
    Users,
    ArrayJoinRenderComponent,
  ],
  providers: [
    UserService
  ]
})
export class SystemModule {
}
