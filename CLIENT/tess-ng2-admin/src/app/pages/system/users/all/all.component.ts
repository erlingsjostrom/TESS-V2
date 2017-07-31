import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";

import { EntityTableField, EntityTable } from 'app/shared/components/entity-table/entity-table.component';
import { UserService, IUser } from "app/shared/resources/users/user.service";
import { ModalService } from 'app/shared/modals/modal.service';

import { Subject } from "rxjs/Subject";

@Component({
	selector: 'all',
	templateUrl: 'all.component.html'
})

export class AllComponent implements OnInit, EntityTable {
	
	private _users: IUser[];
	content: Subject<IUser[]> = new Subject<IUser[]>();
	
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
			label: "Windows User",
			property: "WindowsUser",
			type: "normal"
		},
		{
			label: "Roles",
			property: "Roles",
			type: "array",
			arrayOptions: {
				filterBy: "Name",
				emptyText: "No roles assigned."
			}
		}
	]

	title: string = "Users"

	constructor(
		private _router: Router,
		private _userService: UserService,
		private _modalService: ModalService
	) {}
	
	ngOnInit() { 
		this._userService.getAll().subscribe(
			response => {
				if (response.status == 200) {
					const users = response.json().value;
					this._users = users;
					this.content.next(users);
				}
			}
		)
	}

	onDelete(user: IUser) {
    this._modalService.showConfirmModal(
      "Confirm deletion of " + user.Name,
      "This action is irreversible, do you still want to delete " + user.Name + "?",
      "Delete",
      "Don't delete",
      "btn-danger"
    ).subscribe(
      response => {
        if (response) {
          this._userService.delete(user).subscribe(
            response => {
              if (response.status == 204) {
                var index = this._users.indexOf(user);
                this._users = this._users.filter((val, i) => i != index);
								this.content.next(this._users);
              }
            }
          )
        }
      }
    );
  }

  onEdit(user: IUser) {
    this._router.navigate(['system/users/edit/', user.Id]);
  }

  onAdd() {
    this._router.navigate(['system/users/create']);
  }

	
}