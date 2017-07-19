import { RoleService } from '../../../../shared/roles/role.service';
import { UserService, IUser } from '../../../../shared/users/user.service';
import { Component, OnInit, DoCheck, SimpleChanges } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';


@Component({
	selector: 'edit',
	templateUrl: 'edit.component.html',
	styleUrls: ['./edit.component.scss'],
})

export class EditComponent implements OnInit, DoCheck {
	dataState: DataState<IUser> = new DataState<IUser>();
	user: IUser;
	state: EditComponentState = {
		loading: true,
		modified: false
	}

	checkData: CheckData[] = [];

	constructor(
		private route: ActivatedRoute,
		private router: Router,
		private userService: UserService,
		private roleService: RoleService
	) {
		const id = this.route.snapshot.paramMap.get('id');
		this.userService.get(+id).subscribe(
			user => {
				console.log(user);
				this.dataState.load(user);
				this.user = user;
				this.setCheckData(user);
				this.state.loading = false;
			},
			error => {
				console.log(error);
			}
		);
	}

	ngDoCheck() {
		if(this.user && this.checkDataReady) {
			const roles = this.checkData.filter(cd => cd.checked).map(cd => cd.value);
    	this.user.Roles = roles;
		}
		this.state.modified = this.dataState.isModified();
	}

	ngOnInit() {
	}

	saveChanges() {
    this.userService.put(this.user).subscribe(
			response => {
				const statusCode = response.status;
				if(statusCode == 200){
					this.router.navigate(['system/users']);
				}
			},
			error => {
				console.log(error)
			}
		);
  }
	private checkDataReady: boolean = false;
  setCheckData(user: IUser) {
    this.roleService.get().subscribe(
      data => {
        const availableRoles = data;
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