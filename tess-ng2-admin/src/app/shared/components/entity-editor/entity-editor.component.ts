import { Component, OnInit, DoCheck, Input } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';


@Component({
	selector: 'entity-editor',
	templateUrl: 'entity-editor.component.html',
	styleUrls: ['./entity-editor.component.scss'],
})

export class EntityEditorComponent implements OnInit, DoCheck {
	
	@Input()
	entity: any;
	@Input()
	fields: EntityField[] = [];

	dataState: DataState<any> = new DataState<any>();
	state: EditComponentState = {
		loading: true,
		modified: false
	}
	checkData: CheckData[] = [];

	constructor(
		private router: Router,
	) {}

	ngDoCheck() {
		this.state.modified = this.dataState.isModified();
	}

	ngOnInit() {
		this.dataState.load(this.entity);
	}

	saveChanges() {
		console.log("Saveing changes...");
  }
}

export interface EntityField {
	type: "text" | "number" | "email" | "richtext";
	propertyName: string
	propertyLabel: string
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