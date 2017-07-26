import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { Subject } from "rxjs/Subject";

import { ITemplate, TemplateService } from 'app/shared/resources/templates/template.service';
import { EntityTable, EntityTableField } from 'app/shared/components/entity-table/entity-table.component';
import { ModalService } from "app/shared/modals/modal.service";

@Component({
	selector: 'all',
	templateUrl: 'all.component.html'
})

export class AllComponent implements OnInit, EntityTable {

	private _templates: ITemplate[];
	content: Subject<ITemplate[]> = new Subject<ITemplate[]>();

	tableFields: EntityTableField[] = [
		{
			label: "Id",
			property: "Id",
			type: "normal"
		},
		{
			label: "Name",
			property: "Name",
			type: "normal"
		},
		{
			label: "Description",
			property: "Description",
			type: "normal"
		},
		{
			label: "Type",
			property: "EntityType",
			type: "normal"
		}
	]
	
	title: string = "Templates"

	constructor(
		private _router: Router,
		private _templateService: TemplateService,
		private _modalService: ModalService
	) {}

	ngOnInit() { 
		this._templateService.getAll().subscribe(
			response => {
				if (response.status == 200) {
					const templates = response.json().value
					this._templates = templates;
					this.content.next(templates);
				}
			}
		)
	}

	// onDelete(template: ITemplate) {
  //   this._modalService.showConfirmModal(
  //     "Confirm deletion of " + template.Id,
  //     "This action is irreversible, do you still want to delete " + template.Id + "?",
  //     "Delete",
  //     "Don't delete",
  //     "btn-danger"
  //   ).subscribe(
  //     response => {
  //       if (response) {
  //         this._templateService.delete(template).subscribe(
  //           response => {
  //             if (response.status == 204) {
  //               var index = this._offers.indexOf(template);
  //               this._offers = this._offers.filter((val, i) => i != index);
	// 							this.content.next(this._offers);
  //             }
  //           }
  //         )
  //       }
  //     }
  //   );
  // }

	onEdit(template: ITemplate) {
    this._router.navigate(['templates/edit/', template.Id]);
  }

  onAdd() {
    this._router.navigate(['templates/create/']);
  }

}