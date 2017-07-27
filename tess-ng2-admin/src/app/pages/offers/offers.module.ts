import { DocumentModule } from '../../shared/components/document/document.module';
import { ViewComponent } from './view/view.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { DataTableModule } from 'primeng/primeng';
import { OfferService } from 'app/shared/resources/offers/offer.service';
import { AllComponent } from './all/all.component';
import { EditComponent } from './edit/edit.component';

import { NgaModule } from '../../theme/nga.module';
import { Offers } from './offers.component';
import { routing } from './offers.routing';
import { EntityTableModule } from "app/shared/components/entity-table/entity-table.module";
import { ModalModule } from "app/shared/modals/modal.module";
import { EntityEditorModule } from "app/shared/components/entity-editor/entity-editor.module";
import { DndModule } from 'ng2-dnd';

const COMPONENTS = [
  AllComponent,
  EditComponent,
  ViewComponent
]

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgaModule,
    routing,
    ModalModule,
    EntityTableModule,
    EntityEditorModule,
    DocumentModule,
    DndModule.forRoot()
  ],
  declarations: [
    Offers,
    ... COMPONENTS
  ],
  providers: [
    OfferService,
  ]
})
export class OffersModule {
}
