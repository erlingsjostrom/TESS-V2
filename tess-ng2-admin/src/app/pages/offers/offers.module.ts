import { DataTableModule } from 'primeng/primeng';
import { OfferService } from '../../shared/offers/offer.service';
import { AllOffersComponent } from './all/all-offers.component';
import { NgModule }      from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgaModule } from '../../theme/nga.module';
import { Offers } from './offers.component';
import { routing } from './offers.routing';



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
    AllOffersComponent,
  ],
  providers: [
    OfferService,
  ]
})
export class OffersModule {
}
