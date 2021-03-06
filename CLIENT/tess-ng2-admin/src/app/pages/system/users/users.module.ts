import { AllComponent } from './all/all.component';
import { EntityTableModule } from '../../../shared/components/entity-table/entity-table.module';
import { EntityEditorModule } from '../../../shared/components/entity-editor/entity-editor.module';
import { Users } from './users.component';
import { EditComponent } from './edit/edit.component';
import { NgModule }      from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'app/shared/modals/modal.module';
import { RoleService } from 'app/shared/resources/roles/role.service';
import { UserService } from 'app/shared/resources/users/user.service';
import { NgaModule } from 'app/theme/nga.module';
import { routing } from './users.routing';

const COMPONENTS = [
  AllComponent,
  EditComponent,
]

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgaModule,
    routing,
    ModalModule,
    EntityEditorModule,
    EntityTableModule
  ],
  declarations: [
    Users,
    ... COMPONENTS,
  ],
  providers: [
    UserService,
    RoleService
  ]
})
export class UsersModule {
}
