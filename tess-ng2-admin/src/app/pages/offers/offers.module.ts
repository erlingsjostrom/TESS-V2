import { DataTableModule } from 'primeng/primeng';
import { OfferService } from '../../shared/offers/offer.service';
import { AllComponent } from './all/all.component';
import { EditComponent } from './edit/edit.component';
import { NgModule }      from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgaModule } from '../../theme/nga.module';
import { Offers } from './offers.component';
import { routing } from './offers.routing';
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
    Offers,
    ... COMPONENTS
  ],
  providers: [
    OfferService,
  ]
})
export class OffersModule {
}
