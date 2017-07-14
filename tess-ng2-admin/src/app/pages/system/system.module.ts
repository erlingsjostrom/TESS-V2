import { NgModule }      from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ConfirmModal } from '../../shared/modals/confirm-modal/confirm-modal.component';
import { DefaultModal } from '../../shared/modals/default-modal/default-modal.component';
import { ModalModule } from '../../shared/modals/modal.module';
import { ModalService } from '../../shared/modals/modal.service';
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
    ModalModule
  ],
  declarations: [
    System,
    Users,
  ],
  providers: [
    UserService,
  ]
})
export class SystemModule {
}
