import { Component, OnInit, DoCheck, SimpleChanges } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { ICustomer, CustomerService } from 'app/shared/resources/customers/customer.service';
import { EntityEditor, EntityEditorState } from 'app/shared/components/entity-editor';
import { ModalService } from 'app/shared/modals/modal.service';
import { EntityField } from 'app/shared/components/entity-editor';

import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/switchMap';

@Component({
	selector: 'edit',
	templateUrl: 'edit.component.html',
	styleUrls: ['./edit.component.scss'],
})

export class EditComponent implements EntityEditor {
	private _staging: Subject<number> = new Subject<number>();
	private _stage: number = 0;

	private _customer: ICustomer;
	private _editorFields: EntityField[] = [
		{
			propertyLabel: "Name",
			propertyName: "Name",
			type: "text"
		},
		{
			propertyLabel: "Type",
			propertyName: "Type",
			type: "text"
		},
		{
			propertyLabel: "Corporate Identity Number",
			propertyName: "CorporateIdentityNumber",
			type: "text"
		}
	]

	editorFields: Subject<EntityField[]> = new Subject();
	entity: Subject<ICustomer> = new Subject();

	state: EntityEditorState = {
		loading: false,
		action: "create",
	}
	
	title: string = "Customers"

	constructor(
		private _route: ActivatedRoute,
		private _router: Router,
		private _customerService: CustomerService,
		private _modalService: ModalService
	) {
		this.state.action = "create";
	}
	
	ngOnInit() {
		const id = this._route.snapshot.paramMap.get('id');
		this.initStaging();
		if (id) {
			this.fetchActiveCustomer(+id);
			this.state.action = "edit";
		} else {
			setTimeout(() => {
				this.initNewCustomer();
				this.state.action = "create";
			}, 100)	
		}
	}

	onBack(editedCustomer: ICustomer) { 
		const _editedCustomer = editedCustomer; // You can save the edited customer in local storage
		this._navigateToCustomers();
	}

	onSave(customer: ICustomer) {
		if (this.state.action == "edit") {
			this._customerService.put(customer).subscribe(
				response => {
					if (response.status == 200) {
						this._navigateToCustomers();
					}
				}
			)
		} else {
			this._customerService.post(customer).subscribe(
				response => {
					if (response.status == 201) {
						this._navigateToCustomers();
					}
				}
			)
		}
  }

	private _navigateToCustomers() {
		this._router.navigate(["customers"]);
	}

	private fetchActiveCustomer(id: number) {
		this._customerService.get(+id).subscribe(
			response => {
				if (response.status == 200) {
					this._customer = response.json();
					this._staging.next();
				}
			},
			error => {
				console.log(error);
			}
		);
	}
	
	private initNewCustomer() {
		this._customer = {
			Id: -1,
			Name: "",
			CorporateIdentityNumber: "",
			Type: "",
		}
		this._staging.next();
	}

	private initStaging() {
		this._staging.subscribe(
			stage => {
				this._stage++;
				if(this._stage == 1) {
					this._staging.complete();
				}
			},
			error => console.log(error),
			() => {
				console.log(this._editorFields);
				this.entity.next(this._customer);
				this.editorFields.next(this._editorFields);
			}
		)
	}

}

