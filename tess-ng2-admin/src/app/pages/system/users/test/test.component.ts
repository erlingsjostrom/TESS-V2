import { RoleService } from '../../../../shared/roles/role.service';
import { UserService } from '../../../../shared/users/user.service';
import { EntityEditorComponent, EntityField } from '../../../../shared/components/entity-editor/entity-editor.component';
import { Component, OnInit } from '@angular/core';

@Component({
	selector: 'test',
	templateUrl: 'test.component.html'
})

export class TestComponent implements OnInit {
	myEntity = {
		Name: "Erik",
		UseLogo: false,
		Description: ""
	}
	editorFields: EntityField[] = [
		
		
		// {
		// 	propertyLabel: "Description",
		// 	propertyName: "Name",
		// 	type: "richtext"
		// },
		// {
		// 	propertyLabel: "Email",
		// 	propertyName: "Email",
		// 	type: "email"
		// },
		// {
		// 	propertyLabel: "Phone number",
		// 	propertyName: "PhoneNumber",
		// 	type: "number"
		// },
		
	]
	constructor(
		private userService: UserService,
		private roleService: RoleService
	) {
		this.userService.get(2).subscribe(
			response => {
				console.log(response);
				if (response.status == 200) {
					this.myEntity = response.json();
					this.myEntity.Description = "";
					// this.myEntity.UseLogo = false;
					console.log(this.myEntity);
				}
			}
		)
		this.roleService.get().subscribe(
				response => {
					this.editorFields = [
						{
							propertyLabel: "Name",
							propertyName: "Name",
							type: "text"
						},
						{
							propertyLabel: "Windows User",
							propertyName: "WindowsUser",
							type: "text"
						},
						{
							propertyLabel: "Description",
							propertyName: "Description",
							type: "richtext"
						},
						// {
						// 	propertyLabel: "Use Logo",
						// 	propertyName: "UseLogo",
						// 	type: "checkbox"
						// }
					]
					const test: EntityField = {
						propertyLabel: "Roles",
						propertyName: "Roles",
						type: "checkbox",
						availableValues: response
					}
					this.editorFields.push(test);
					
					console.log("Roles loaded and insertetd");
				},
				error => console.log(error)
		);
	}
	ngOnInit() { }
}