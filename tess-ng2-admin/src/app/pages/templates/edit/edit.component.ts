import { Component, OnInit, DoCheck, SimpleChanges } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { ITemplate, TemplateService } from 'app/shared/resources/templates/template.service';
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

	private _template: ITemplate;
	private _editorFields: EntityField[] = [
		{
			propertyLabel: "Name",
			propertyName: "Name",
			type: "text"
		},
		{
			propertyLabel: "Description",
			propertyName: "Description",
			type: "text"
		},
		{
			propertyLabel: "Type",
			propertyName: "EntityType",
			type: "text"
		},
	]

	editorFields: Subject<EntityField[]> = new Subject();
	entity: Subject<ITemplate> = new Subject();

	state: EntityEditorState = {
		loading: false,
		action: "create",
	}

	constructor(
		private _route: ActivatedRoute,
		private _router: Router,
		private _templateService: TemplateService,
		private _modalService: ModalService
	) {
		this.state.action = "create";
	}
	
	ngOnInit() {
		const id = this._route.snapshot.paramMap.get('id');
		this.initStaging();
		if (id) {
			this.fetchActiveTemplate(+id);
			this.state.action = "edit";
		} else {
			setTimeout(() => {
				this.initNewTemplate();
				this.state.action = "create";
			}, 100)	
		}
	}

	onBack(editedTemplate: ITemplate) { 
		const _editedTemplate = editedTemplate; // You can save the edited offer in local storage
		this._navigateToTemplates();
	}

	onSave(template: ITemplate) {
		if (this.state.action == "edit") {
			this._templateService.put(template).subscribe(
				response => {
					if (response.status == 200) {
						this._navigateToTemplates();
					}
				}
			)
		} else {
			this._templateService.post(template).subscribe(
				response => {
					if (response.status == 201) {
						this._navigateToTemplates();
					}
				}
			)
		}
   	}
	
	private initNewTemplate() {
		this._template = {
			Id: -1,
			Name: "",
  			Description: "",
  			EntityType: ""
		}
		this._staging.next();
	}

	private _navigateToTemplates() {
		this._router.navigate(["templates"]);
	}

	private fetchActiveTemplate(id: number) {
		this._templateService.get(+id).subscribe(
			response => {
				if (response.status == 200) {
					this._template = response.json();
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
				this.entity.next(this._template);
				this.editorFields.next(this._editorFields);
			}
		)
	}

}

