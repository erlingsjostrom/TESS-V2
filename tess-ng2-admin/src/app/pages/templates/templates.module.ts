import { NgModule }      from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { AllComponent } from './all/all.component';
import { EditComponent } from './edit/edit.component';
import { TemplateService } from 'app/shared/resources/templates/template.service';

import { NgaModule } from '../../theme/nga.module';
import { Templates } from './templates.component';
import { routing } from './templates.routing';
import { EntityTableModule } from "app/shared/components/entity-table/entity-table.module";
import { ModalModule } from "app/shared/modals/modal.module";
import { EntityEditorModule } from "app/shared/components/entity-editor/entity-editor.module";

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
    EntityTableModule,
    EntityEditorModule
  ],
  declarations: [
    Templates,
    ... COMPONENTS
  ],
  providers: [
    TemplateService,
  ]
})
export class TemplatesModule {
}
