import { NgModule }      from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../shared/users/user.service';
import { NgaModule } from '../../theme/nga.module';
import { Users } from './components/users/users.component';
import { System } from './system.component';
import { routing } from './system.routing';

import { DataTableModule } from 'primeng/primeng';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgaModule,
    routing,
    DataTableModule,
  ],
  declarations: [
    System,
    Users,
  ],
  providers: [
    UserService
  ]
})
export class SystemModule {
}
