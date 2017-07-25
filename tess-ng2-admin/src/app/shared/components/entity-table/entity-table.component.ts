import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Subject } from "rxjs/Subject";

const EMPTY_FUNC = () => {console.log("test")};

@Component({
	selector: 'entity-table',
	templateUrl: 'entity-table.component.html',
	styleUrls: ['./entity-table.component.scss']
})

export class EntityTableComponent implements OnInit {
	
	@Output() 
	onAdd: EventEmitter<any> = new EventEmitter();
	@Output() 
	onEdit: EventEmitter<any> = new EventEmitter();
	@Output() 
	onDelete: EventEmitter<any> = new EventEmitter();
	
	@Input()
	editable: boolean = true;
	
	private _content: any[] = [];
	@Input()
	set content(value) {
		this._content = value;
		if (this.tableFields.length == 0 && this.content != null) {
			this.autoInitTableFields();
		}
	}
	get content() {
		return this._content;
	}
	
	@Input()
	tableFields: EntityTableField[] = []
	
	@Input() title: string;

	state = {
		loading: false
	}

	ngOnInit() {
		
	}

	add(entity) {
		this.onAdd.emit(entity);
	}
	edit(entity) {
		this.onEdit.emit(entity);
	}
	delete(entity) {
		this.onDelete.emit(entity);
	}

	private autoInitTableFields() {
		const entity = this.content[0];
		for (let pName in entity) {
			this.tableFields.push({
				label: this.prettifyName(pName),
				property: pName,
				type: "normal"
			})
		}
	}
	
	// transform "camelCaseStr" to "Camel Case Str"
	private prettifyName(str): string {
		return str.replace(/([A-Z])/g, ' $1')
						  .replace(/^./, (str) => { 
								return str.toUpperCase(); 
							});
	}
}

export interface EntityTableField {
	property: any,
	label: string,
	type: "normal" | "array",
	arrayOptions?: {
		filterBy: string,
		emptyText: string
	}
}

export interface EntityTable {
	content: Subject<any[]> 
}