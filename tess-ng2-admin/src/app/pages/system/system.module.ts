import { NgModule }      from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ConfirmModal } from '../../shared/modals/confirm-modal/confirm-modal.component';
import { DefaultModal } from '../../shared/modals/default-modal/default-modal.component';
import { ModalModule } from '../../shared/modals/modal.module';
import { ModalService } from '../../shared/modals/modal.service';
import { ArrayMapJoinPipe } from '../../shared/pipes/array-map-join.pipe';
import { ArraySortPipe } from '../../shared/pipes/sort.pipe';
import { RoleService } from '../../shared/roles/role.service';
import { UserService } from '../../shared/users/user.service';
import { NgaModule } from '../../theme/nga.module';
import { RolesModal } from './components/users/modals/roles-modal/roles-modal.component';
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
  entryComponents: [
    RolesModal,
  ],
  declarations: [
    ArrayMapJoinPipe,
    ArraySortPipe,
    System,
    Users,
    RolesModal,
  ],
  providers: [
    UserService,
    RoleService
  ]
})
export class SystemModule {
}
