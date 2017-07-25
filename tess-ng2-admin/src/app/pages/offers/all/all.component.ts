import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { Subject } from "rxjs/Subject";

import { IOffer, OfferService } from 'app/shared/resources/offers/offer.service';
import { EntityTable, EntityTableField } from 'app/shared/components/entity-table/entity-table.component';
import { ModalService } from "app/shared/modals/modal.service";

@Component({
	selector: 'all',
	templateUrl: 'all.component.html'
})

export class AllComponent implements OnInit, EntityTable {

	private _offers: IOffer[];
	content: Subject<IOffer[]> = new Subject<IOffer[]>();

	tableFields: EntityTableField[] = [
		{
			label: "Id",
			property: "Id",
			type: "normal"
		},
		{
			label: "Status",
			property: "Status",
			type: "normal"
		}
	]
	
	title: string = "Offers"

	constructor(
		private _router: Router,
		private _offerService: OfferService,
		private _modalService: ModalService
	) {}

	ngOnInit() { 
		this._offerService.getAll().subscribe(
			response => {
				if (response.status == 200) {
					const offers = response.json().value
					this._offers = offers;
					this.content.next(offers);
				}
			}
		)
	}

	onDelete(offer: IOffer) {
    this._modalService.showConfirmModal(
      "Confirm deletion of " + offer.Id,
      "This action is irreversible, do you still want to delete " + offer.Id + "?",
      "Delete",
      "Don't delete",
      "btn-danger"
    ).subscribe(
      response => {
        if (response) {
          this._offerService.delete(offer).subscribe(
            response => {
              if (response.status == 204) {
                var index = this._offers.indexOf(offer);
                this._offers = this._offers.filter((val, i) => i != index);
								this.content.next(this._offers);
              }
            }
          )
        }
      }
    );
  }

	onEdit(offer: IOffer) {
    this._router.navigate(['offers/edit/', offer.Id]);
  }

  onAdd() {
    this._router.navigate(['offers/create']);
  }


}