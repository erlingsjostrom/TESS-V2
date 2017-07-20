import { Component, OnInit, DoCheck, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';
import { Subject } from "rxjs/Subject";


@Component({
	selector: 'entity-editor',
	templateUrl: 'entity-editor.component.html',
	styleUrls: ['./entity-editor.component.scss'],
})

export class EntityEditorComponent implements OnInit, DoCheck {
	private _subject: Subject<boolean> = new Subject();
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


	dataState: DataState<any> = new DataState<any>();
	state: EditComponentState = {
		loading: true,
		modified: false
	}

	constructor(
		private router: Router,
	) {}

	// TODO: find replacement for this pls, it fires on mouse move events :(
	ngDoCheck() {
		if(this.checkDataReady) {
			for(let cde of this._checkDataEntities) {
				const data = cde.data.filter(cd => cd.checked).map(cd => cd.value);
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
		this._subject.next(true);
  }
	goBack() {
		this._subject.next(false);
  }

	private checkDataReady = false;
	private handleCheckData(fields: EntityField[]) {
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
			} else {
				this._checkDataEntities.push({
					name: propName,
					data: [
						{
							checked: entity,
							value: {
								Name: field.propertyLabel
							}
						}
					]
				});
			}
		}
		this.checkDataReady = true;
	}
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