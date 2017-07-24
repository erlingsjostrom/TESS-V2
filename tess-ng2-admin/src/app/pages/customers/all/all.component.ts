
import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { Subject } from "rxjs/Subject";

import { ModalService } from "app/shared/modals/modal.service";
import { ICustomer, CustomerService } from 'app/shared/resources/customers/customer.service';
import { EntityTable, EntityTableField } from 'app/shared/components/entity-table/entity-table.component';


@Component({
	selector: 'all',
	templateUrl: 'all.component.html'
})

export class AllComponent implements OnInit, EntityTable {

	private _customers: ICustomer[];
	content: Subject<ICustomer[]> = new Subject<ICustomer[]>();

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
	
		constructor(
		private _router: Router,
		private _customerService: CustomerService,
		private _modalService: ModalService
	) {}

	ngOnInit() { 
		this._customerService.getAll().subscribe(
			response => {
				if (response.status == 200) {
					const customers = response.json().value
					this._customers = customers;
					this.content.next(customers);
				}
			}
		)
	}

	onDelete(customer: ICustomer) {
    this._modalService.showConfirmModal(
      "Confirm deletion of " + customer.Id,
      "This action is irreversible, do you still want to delete " + customer.Id + "?",
      "Delete",
      "Don't delete",
      "btn-danger"
    ).subscribe(
      response => {
        if (response) {
          this._customerService.delete(customer).subscribe(
            response => {
              if (response.status == 204) {
                var index = this._customers.indexOf(customer);
                this._customers = this._customers.filter((val, i) => i != index);
								this.content.next(this._customers);
              }
            }
          )
        }
      }
    );
  }

	onEdit(customer: ICustomer) {
    this._router.navigate(['customers/edit/', customer.Id]);
  }

  onAdd() {
    this._router.navigate(['customers/create']);
  }


}