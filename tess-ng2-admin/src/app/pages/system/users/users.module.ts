import { LoadingService } from '../../../shared/components/loading/loading.service';
import { LoadingComponent } from '../../../shared/components/loading/loading.component';
import { Users } from './users.component';
import { EditComponent } from './edit/edit.component';
import { NgModule }      from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ModalModule } from '../../../shared/modals/modal.module';
import { ModalService } from '../../../shared/modals/modal.service';
import { ArrayMapJoinPipe } from '../../../shared/pipes/array-map-join.pipe';
import { ArraySortPipe } from '../../../shared/pipes/sort.pipe';
import { RoleService } from '../../../shared/roles/role.service';
import { UserService } from '../../../shared/users/user.service';
import { NgaModule } from '../../../theme/nga.module';
import { AllUsersComponent } from './all/all-users.component';
import { routing } from './users.routing';

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
    ArrayMapJoinPipe,
    ArraySortPipe,
    Users,
    AllUsersComponent,
    EditComponent,
  ],
  providers: [
    UserService,
    RoleService,
  ]
})
export class UsersModule {
}
