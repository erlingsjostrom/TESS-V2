import { ModalService } from '../../modals/modal.service';
import { Component, OnInit, DoCheck, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { CapitalizePipe } from '../../../shared/pipes/capitalize.pipe';

import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';
import { Subject } from "rxjs/Subject";


@Component({
	selector: 'entity-editor',
	templateUrl: 'entity-editor.component.html',
	styleUrls: ['./entity-editor.component.scss'],
})

export class EntityEditorComponent implements OnInit, DoCheck {
	private _entity: any;
	private _fields: EntityField[] = [];
	
	private _checkDataEntities: CheckDataEntity[] = [];
	get checkDataEntities() {
		return this._checkDataEntities;
	}
	@Input() 
	set entity(value: any) {
		this._entity = value;
		this.dataState.load(this._entity);
	}
	get entity(){
		return this._entity;
	}
	
	@Input()
	set fields(fields: EntityField[]) {
		this._fields = fields;
		this.handleCheckData(this._fields);
	}
	get fields() {
		return this._fields;
	}

	@Output() onSave: EventEmitter<any> = new EventEmitter()
	@Output() onBack: EventEmitter<any> = new EventEmitter()


	dataState: DataState<any> = new DataState<any>();
	state: EditComponentState = {
		loading: true,
		modified: false
	}

	constructor(
		private router: Router,
		private modalService: ModalService
	) {}

	// TODO: find replacement for this pls, it fires on mouse move events :(
	ngDoCheck() {
		if(this.checkDataReady) {
			for(let cde of this._checkDataEntities) {
				let data: any = cde.data.filter(cd => cd.checked).map(cd => cd.value);
				if (typeof cde.data[0].value === 'boolean'){
					data = data.length !== 0;
				}
				this.entity[cde.name] = data;
			}
		}
		this.state.modified = this.dataState.isModified();
	}

	ngOnInit() {
		this.dataState.load(this.entity);
		this.state.loading = false;
	}

	saveChanges() {
		this.onSave.emit(this.entity);
  }

	goBack() {
		if(this.state.modified) {
			this.modalService.showConfirmModal(
				"Do you really want to go back?",
	      "You have unsaved changes, are you really sure you want to exit without saving?",
	      "Discard changes and go back",
	      "Cancel",
	      "btn-warning-dark"
			).subscribe(
				response => {
					if (response) {
						this.onBack.emit(this.entity); // Pass along entity for local storage save purposes
					}
				}
			);
		} else {
			this.onBack.emit(); 
		}				
  }

	private checkDataReady = false;
	private handleCheckData(fields: EntityField[]) {
		if (fields) {
			const checkDataFields = fields.filter(f => f.type == "checkbox");
			for(let field of checkDataFields) {
				const propName = field.propertyName;
				const entity = this.entity[propName];
				if (entity instanceof Array) {
					if(!field.availableValues) {
						throw new EntityFieldError(`For type == 'checkbox' and entity type Array a list of 
																				available check options is required`);
					}
					const entityArr = entity;
					const checkDataArr: CheckData[] = field.availableValues.map(item => {
						return {
							checked: entityArr.filter(e => JSON.stringify(e) === JSON.stringify(item)).length > 0,
							value: item
						}
					});
					this._checkDataEntities.push({
						name: propName,
						data: checkDataArr
					});
				} else if (typeof entity === 'boolean') {
					this._checkDataEntities.push({
						name: propName,
						data: [
							{
								checked: entity,
								value: entity
							}
						]
					});
				} else {
					throw new EntityFieldError(`Unsupported EntityField checkbox type`);
				}
			}
			this.checkDataReady = true;
		}
	}
}

export interface EntityEditor {
	editorFields: Subject<EntityField[]>,
	entity: Subject<any>,
	state: {loading: boolean, action: string}
}

export interface EntityField {
	type: "text" | "number" | "email" | "richtext" | "checkbox";
	propertyName: string
	propertyLabel: string,
	availableValues?: Object[]
}

export class EntityFieldError extends Error {
	constructor(m: string) {
		super(m);
	
		// Set the prototype explicitly.
		Object.setPrototypeOf(this, EntityFieldError.prototype);
	}
}

export interface CheckDataEntity {
	name: string,
	data: CheckData[]
}

export interface CheckData {
  checked: boolean,
  value: any
}

export interface EditComponentState {
	loading: boolean;
	modified: boolean;
}

export class DataState<T> {
  data: T;
  private rawData: T;
  
  load(data: T) {
    this.data = data;
    this.rawData = this.cloneData(data);
  }

  isModified(): boolean {
    return !(JSON.stringify(this.data) === JSON.stringify(this.rawData));
  }
  
  private cloneData(data: T): T {
    return Object.assign({}, JSON.parse(JSON.stringify(data)));
  }
}