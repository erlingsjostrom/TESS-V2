import { Component, OnInit, DoCheck, SimpleChanges } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { ModalService } from 'app/shared/modals/modal.service';
import { EntityField, EntityEditor, EntityEditorState } from 'app/shared/components/entity-editor';
import { RoleService, IRole } from 'app/shared/resources/roles/role.service';
import { UserService, IUser } from 'app/shared/resources/users/user.service';

import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/switchMap';

@Component({
	selector: 'edit',
	templateUrl: 'edit.component.html',
	styleUrls: ['./edit.component.scss'],
})

export class EditComponent implements OnInit, EntityEditor {
	private _staging: Subject<number> = new Subject<number>();
	private _stage: number = 0;

	private _user: IUser;
	private _roles: IRole[] = [];
	private _editorFields: EntityField[] = [
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
	]

	entity: Subject<IUser> = new Subject();
	editorFields: Subject<EntityField[]> = new Subject();
	
	state: EntityEditorState = {
		loading: false,
		action: "create",
	}

	constructor(
		private _route: ActivatedRoute,
		private _router: Router,
		private _userService: UserService,
		private _roleService: RoleService,
		private _modalService: ModalService
	) {}

	ngOnInit() {
		const id = this._route.snapshot.paramMap.get('id');
		this.initStaging();
		if (id) {
			this.fetchActiveUser(+id);
			this.state.action = "edit";
		} else {
			this.initNewUser();
			this.state.action = "create";
		}
		this.fetchRoles();
	}
	
	onBack(editedUser: IUser) { 
		const _editedUser = editedUser; // You can save the edited user in local storage
		this._navigateToUsers();
	}

	onSave(user: IUser) {
		if (this.state.action == "edit") {
			this._userService.put(user).subscribe(
				response => {
					if (response.status == 200) {
						this._navigateToUsers();
					}
				}
			)
		} else {
			this._userService.post(user).subscribe(
				response => {
					if (response.status == 201) {
						this._navigateToUsers();
					}
				}
			)
		}
  }

	private _navigateToUsers() {
		this._router.navigate(["system/users"]);
	}
	
	private fetchActiveUser(id: number) {
		this._userService.get(+id).subscribe(
			response => {
				if (response.status == 200) {
					this._user = response.json();
					this._staging.next();
				}
			},
			error => {
				console.log(error);
			}
		);
	}

	private fetchRoles() {
		this._roleService.getAll().subscribe(
			response => {
				if (response.status == 200) {
					this._roles = response.json().value;
					this._editorFields.push({
						propertyLabel: "Roles",
						propertyName: "Roles",
						type: "checkbox",
						availableValues: this._roles
					});
					this._staging.next();
				}
			},
			error => {
				console.log(error);
			}
		);
	}

	private initNewUser() {
		this._user = {
			Id: -1,
			Name: "",
			WindowsUser: "",
			Roles: []
		}
		this._staging.next();
	}

	private initStaging() {
		this._staging.subscribe(
			stage => {
				this._stage++;
				if(this._stage == 2) {
					this._staging.complete();
				}
			},
			error => console.log(error),
			() => {
				this.entity.next(this._user);
				this.editorFields.next(this._editorFields);
			}
		)
	}

}

