import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { ModalService, ModalSize, ModalType } from '../../../../shared/modals/modal.service';
import { IRole, IUser, UserService } from '../../../../shared/users/user.service';
import { RolesModal } from './modals/roles-modal/roles-modal.component';


@Component({
  selector: 'users-component',
  templateUrl: './all-users.component.html',
  styleUrls: ['./all-users.component.scss'],
  encapsulation: ViewEncapsulation.None,
})

export class AllUsersComponent {
  state = {
    edited: false,
    loading: true,
    editMode: false,
  }

  content: DataTableList<IUser> = new DataTableList<IUser>();
  
  constructor(
    private userService: UserService,
    private modalService: ModalService,
    private router: Router
  ) {
    this.userService.getAll().subscribe((users) => {
      this.content.load(users);
      this.state.loading = false;
    })
  }
  
  edit(id: number) {
    this.router.navigate(['system/users/edit/', id]);
  }

}

export class DataTableList<T> {
  data: T[] = [];
  private rawData: T[] = [];
  
  load(data: T[]) {
    this.data = data;
    this.rawData = this.cloneData(data);
  }

  modified(): T[] {
    return this.data.filter((item, index: number) => {
      return !(JSON.stringify(item) === JSON.stringify(this.rawData[index]));
    });
  }

  isModified(): boolean {
    return this.modified().length > 0;
  }
  
  private cloneData(data: T[]): T[] {
    let clonedData: T[] = [];
    data.forEach(item => {
      clonedData.push(Object.assign({}, JSON.parse(JSON.stringify(item))));
    });
    return clonedData;
  }
}