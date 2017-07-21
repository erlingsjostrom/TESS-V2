import { DataTableModule } from 'primeng/primeng';
import { OfferService } from '../../shared/offers/offer.service';
import { AllOffersComponent } from './all/all-offers.component';
import { EditComponent } from './edit/edit.component';
import { NgModule }      from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgaModule } from '../../theme/nga.module';
import { Offers } from './offers.component';
import { routing } from './offers.routing';

const COMPONENTS = [
  AllOffersComponent,
  EditComponent,
]

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgaModule,
    routing,
    DataTableModule,
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
