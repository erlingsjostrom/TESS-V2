import { Component, OnInit, DoCheck, SimpleChanges } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import {  OfferService, IOffer, IOfferContent } from 'app/shared/resources/offers/offer.service';
import { MultipleEntityEditor, EntityEditorState } from 'app/shared/components/entity-editor';
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

export class EditComponent implements MultipleEntityEditor {
	private _staging: Subject<number> = new Subject<number>();
	private _stage: number = 0;

	private _offer: IOffer;
	private _editorFields: EntityField[] = [
		{
			propertyLabel: "Status",
			propertyName: "Status",
			type: "text"
		},
		{
			propertyLabel: "Title",
			propertyName: "Title",
			type: "text"
		},
		{
			propertyLabel: "Valid Through",
			propertyName: "ValidThrough",
			type: "text"
		},
	]

	editorFields = {
		head: new Subject<EntityField[]>(),
		contents: new Subject<EntityField[]>(),
	}
	entity = {
		head: new Subject<IOffer>(),
		contents: new Subject<IOfferContent[]>(),
	} 

	state: EntityEditorState = {
		loading: false,
		action: "create",
	}

	constructor(
		private _route: ActivatedRoute,
		private _router: Router,
		private _offerService: OfferService,
		private _modalService: ModalService
	) {
		this.state.action = "create";
	}
	
	ngOnInit() {
		const id = this._route.snapshot.paramMap.get('id');
		this.initStaging();
		if (id) {
			this.fetchActiveOffer(+id);
			this.state.action = "edit";
		} else {
			setTimeout(() => {
				this.initNewOffer();
				this.state.action = "create";
			}, 100)	
		}
	}

	onBack(editedOffer: IOffer) { 
		const _editedOffer = editedOffer; // You can save the edited offer in local storage
		this._navigateToOffers();
	}

	onSave(offer: IOffer) {
		if (this.state.action == "edit") {
			this._offerService.put(offer).subscribe(
				response => {
					if (response.status == 200) {
						this._navigateToOffers();
					}
				}
			)
		} else {
			this._offerService.post(offer).subscribe(
				response => {
					if (response.status == 201) {
						this._navigateToOffers();
					}
				}
			)
		}
  }
	
	private initNewOffer() {
		this._offer = {
			Id: -1,
			Status: "",
			Title: "",
			Contents: []
		}
		this._staging.next();
	}

	private _navigateToOffers() {
		this._router.navigate(["offers"]);
	}

	private fetchActiveOffer(id: number) {
		this._offerService.getWithContents(+id).subscribe(
			response => {
				if (response.status == 200) {
					this._offer = response.json();
					this._offer.Contents = this._offer.Contents
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
				this.entity.head.next(this._offer);
				const test = this._offer.Contents.filter(c => c.Type == "TextItem").map(c => c.TextItems[0]);
				console.log(test[0]);
				this.entity.contents.next(test);
				this.editorFields.head.next(this._editorFields);
				this.editorFields.contents.next([{
						propertyLabel: "Text",
						propertyName: "Data",
						type: "richtext"
					}])
			}
		)
	}

}

