import { ObservableInput } from 'rxjs/Observable';
import { Observable, Subscription } from 'rxjs/Rx';
import { Offers } from '../';
import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { IOffer, OfferService } from '../../../shared/offers/offer.service';


@Component({
  selector: 'offerss-component',
  templateUrl: './all-offers.component.html',
  styleUrls: ['./all-offers.component.scss'],
  encapsulation: ViewEncapsulation.None,
})

export class AllOffersComponent {
  state = {
    loading: true,
  }

  content: IOffer[] = [];
  
  constructor(
    private offerService: OfferService,
    private router: Router
  ) {
    this.offerService.getAll().subscribe(
      response => {
        if(response.status == 200){
          this.content = response.json().value;
          this.state.loading = false;
        }
      },
      error => {
        this.state.loading = false;
      }
    );
  }

  edit(id: number) {
    this.router.navigate(['offers/edit/', id]);
  }
}
