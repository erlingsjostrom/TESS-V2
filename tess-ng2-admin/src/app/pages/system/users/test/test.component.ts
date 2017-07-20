import { RoleService } from '../../../../shared/roles/role.service';
import { IUser, UserService } from '../../../../shared/users/user.service';
import { EntityEditorComponent, EntityField } from '../../../../shared/components/entity-editor/entity-editor.component';
import { Component, OnInit } from '@angular/core';

import { Location } from '@angular/common';

@Component({
	selector: 'test',
	templateUrl: 'test.component.html'
})

export class TestComponent implements OnInit {
	myEntity = {};

	editorFields: EntityField[] = [];

	constructor(
		private userService: UserService,
		private roleService: RoleService,
		private location: Location
	) {
		this.userService.get(2).subscribe(
			response => {
				if (response.status == 200) {
					//this.myEntity = response.json();
					this.myEntity = {
						IsNull: true
					}
					this.editorFields = [
						{
							propertyLabel: "Is null",
							propertyName: "IsNull",
							type: "checkbox"
						}
					]
				// 	this.roleService.get().subscribe(
				// 		response => {
				// 			let ef: EntityField[] = [
				// 				{
				// 					propertyLabel: "Name",
				// 					propertyName: "Name",
				// 					type: "text"
				// 				},
				// 				{
				// 					propertyLabel: "Windows User",
				// 					propertyName: "WindowsUser",
				// 					type: "text"
				// 				},
				// 				// {
				// 				// 	propertyLabel: "Use Logo",
				// 				// 	propertyName: "UseLogo",
				// 				// 	type: "checkbox"
				// 				// }
				// 			];
				// 			const test: EntityField = {
				// 				propertyLabel: "Roles",
				// 				propertyName: "Roles",
				// 				type: "checkbox",
				// 				availableValues: response
				// 			}

				// 			ef.push(test);
				// 			this.editorFields = ef;
				// 		},
				// 		error => console.log(error)
				// );
				}
			}
		)
	
	}
	ngOnInit() { }

	onSave(user: IUser) {
		console.log("Updated User: ", user);
		this.location.back();
	} 

	onBack(user: IUser) {
		if (user) {
			console.log("Unsaved User: ", user);
		}
		this.location.back();
	}
}