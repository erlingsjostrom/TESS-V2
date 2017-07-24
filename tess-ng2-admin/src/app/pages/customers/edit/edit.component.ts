import { ICustomer, CustomerService } from '../../../shared/customers/customer.service';
import { EntityEditor, EntityEditorState } from '../../../shared/components/entity-editor';
import { ModalService } from '../../../shared/modals/modal.service';
import { EntityField } from '../../../shared/components/entity-editor';
import { Component, OnInit, DoCheck, SimpleChanges } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';

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
			propertyLabel: "Status",
			propertyName: "Status",
			type: "text"
		},
		{
			propertyLabel: "Valid Through",
			propertyName: "ValidThrough",
			type: "text"
		},
	]

	editorFields: Subject<EntityField[]> = new Subject();
	entity: Subject<ICustomer> = new Subject();

	state: EntityEditorState = {
		loading: false,
		action: "create",
	}

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
			this.state.action = "create";
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
				console.log(this._customer);
				this.entity.next(this._customer);
				this.editorFields.next(this._editorFields);
			}
		)
	}

}

