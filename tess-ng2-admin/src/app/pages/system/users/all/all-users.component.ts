import { Users } from '../';
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
    loading: true,
  }

  content: IUser[] = [];
  
  constructor(
    private userService: UserService,
    private modalService: ModalService,
    private router: Router
  ) {
    this.userService.getAll().subscribe(
      response => {
        if(response.status == 200){
          this.content = response.json().value;
          this.state.loading = false;
        }
      },
      error => {
        this.state.loading = false;
      }
    );
  }
  
  removeUser(user: IUser){
    this.userService.delete(user).subscribe(
			response => {
        console.log(response.status);
        if(response.status == 204){
          var index = this.content.indexOf(user);
          this.content = this.content.filter((val, i) => i!=index);
        }
      })
  }

  edit(id: number) {
    this.router.navigate(['system/users/edit/', id]);
  }
}
