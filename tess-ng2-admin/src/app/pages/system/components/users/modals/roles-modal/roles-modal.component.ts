import { RoleService } from '../../../../../../shared/roles/role.service';
import { IRole, IUser } from '../../../../../../shared/users/user.service';

import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

export interface SetData {
  setData(data: any),
}

@Component({
  selector: 'roles-modal',
  styleUrls: [('./roles-modal.component.scss')],
  templateUrl: './roles-modal.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class RolesModal implements OnInit, SetData {

  checkData: CheckData[] = [];

  private rolesRaw: IRole[] = [];
  private user: IUser;
  constructor(
    private activeModal: NgbActiveModal,
    private roleService: RoleService
  ) {
    
  }

  ngOnInit() {}

  closeModal() {
    this.activeModal.close();
  }

  saveChanges() {
    const roles = this.checkData.filter(cd => cd.checked).map(cd => cd.value);
    this.user.Roles = roles;
    this.closeModal();
  }

  setData(user: IUser) {
    this.roleService.get().subscribe(
      data => {
        const availableRoles = data;
        this.checkData = availableRoles.map(cdr => { 
          return {
            checked: user.Roles.filter(r => r.Id == cdr.Id).length > 0, 
            value: cdr
          } 
        });
      },
      error => console.log(error)
    )
    this.user = user;
  }
  
}
export interface CheckData {
  checked: boolean,
  value: any
}
