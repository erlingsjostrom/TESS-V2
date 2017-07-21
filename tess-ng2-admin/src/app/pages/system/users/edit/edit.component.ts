import { ModalService } from '../../../../shared/modals/modal.service';
import { EntityField } from '../../../../shared/components/entity-editor';
import { RoleService, IRole } from '../../../../shared/roles/role.service';
import { UserService, IUser } from '../../../../shared/users/user.service';
import { Component, OnInit, DoCheck, SimpleChanges } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';

@Component({
	selector: 'edit',
	templateUrl: 'edit.component.html',
	styleUrls: ['./edit.component.scss'],
})

export class EditComponent implements OnInit {
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

	user: Subject<IUser> = new Subject();
	editorFields: Subject<EntityField[]> = new Subject();
	
	state = {
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
			this.state.action = "Edit";
		} else {
			this.initNewUser();
			this.state.action = "Create";
		}
		this.fetchRoles();
	}
	
	onBack(editedUser: IUser) { 
		const _editedUser = editedUser; // You can save the edited user in local storage
		this._navigateToUsers();
	}

	onSave(user: IUser) {
		if (this.state.action == "Edit") {
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
		this._roleService.get().subscribe(
			roles => {
				this._roles = roles;
				this._editorFields.push({
					propertyLabel: "Roles",
					propertyName: "Roles",
					type: "checkbox",
					availableValues: this._roles
				});
				this._staging.next();
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
				this.user.next(this._user);
				this.editorFields.next(this._editorFields);
			}
<<<<<<< HEAD
		)
	}
=======
		);
  }
	private checkDataReady: boolean = false;
  setCheckData(user: IUser) {
    this.roleService.get().subscribe(
      response => {
        const availableRoles = response.json();
        this.checkData = availableRoles.map(cdr => { 
          return {
            checked: user.Roles.filter(r => r.Id == cdr.Id).length > 0, 
            value: cdr
          } 
        }).sort();
				this.checkDataReady = true;
      },
      error => console.log(error)
    )
  }
}

export interface CheckData {
  checked: boolean,
  value: any
}
>>>>>>> 6c46c5d61e8401e1733c00798b62493cefda66f7

}

