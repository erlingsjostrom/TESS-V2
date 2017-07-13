import { Component, OnInit } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { IUser, UserService } from '../../../../shared/users/user.service';
import { ArrayJoinRenderComponent } from './array-join-render.component';


@Component({
  selector: 'users-component',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})

export class Users {
  editEnable: boolean = false;
  source: LocalDataSource = new LocalDataSource();

  settings = {
    // hideSubHeader: true,
    actions: {
      edit: false,
      delete: false,
    },
    add: {
      addButtonContent: '<i class="ion-ios-plus-outline"></i>',
      createButtonContent: '<i class="ion-checkmark"></i>',
      cancelButtonContent: '<i class="ion-close"></i>',
    },
    edit: {
      editButtonContent: '<i class="ion-edit"></i>',
      saveButtonContent: '<i class="ion-checkmark"></i>',
      cancelButtonContent: '<i class="ion-close"></i>',
    },
    delete: {
      deleteButtonContent: '<i class="ion-trash-a"></i>',
      confirmDelete: true
    },
    columns: {
      Id: {
        title: 'ID',
        type: 'text',
        width: '50px',
        editable: false,
      },
      Name: {
        title: 'Name',
        type: 'text',
      },
      WindowsUser: {
        title: 'Windows User',
        type: 'text'
      },
      // Roles : {
      //   title: 'Roles',
      //   type: 'custom',
      //   renderComponent: ArrayJoinRenderComponent,
      // }
      // CreatedAt: {
      //   title: 'Created at',
      //   type: 'date'
      // },
      // UpdatedAt: {
      //   title: 'Updated at',
      //   type: 'date'
      // }
    }
  };

  constructor(private userService: UserService) {
    console.log(this.source);
    this.userService.get().subscribe((users) => {
      this.source.load(users);
    })
  }

  onDeleteConfirm(event): void {
    if (window.confirm('Are you sure you want to delete?')) {
      event.confirm.resolve();
    } else {
      event.confirm.reject();
    }
  }

  onEditToggle() {
    this.editEnable = !this.editEnable;
    // this.settings.hideSubHeader = !this.settings.hideSubHeader;
    this.settings.actions.edit = !this.settings.actions.edit;
    this.settings.actions.delete = !this.settings.actions.delete;

    this.settings = Object.assign({}, this.settings);
  }
}
