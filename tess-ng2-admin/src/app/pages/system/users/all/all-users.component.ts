import { ObservableInput } from 'rxjs/Observable';
import { Observable, Subscription } from 'rxjs/Rx';
import { Users } from '../';
import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { ModalService, ModalSize, ModalType } from '../../../../shared/modals/modal.service';
import { IRole, IUser, UserService } from '../../../../shared/users/user.service';


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
  
  removeUser(user: IUser) {
    this.modalService.showConfirmModal(
      "Confirm deletion of " + user.Name,
      "This action is irreversible, do you still want to delete " + user.Name + "?",
      "Delete",
      "Don't delete",
      "btn-danger"
    ).subscribe(
      response => {
        if(response){
          this.userService.delete(user).subscribe(
            response => {
              if(response.status == 204){
                var index = this.content.indexOf(user);
                this.content = this.content.filter((val, i) => i != index);
              }
            }
          )
        }
      }
    );
  }

  edit(id: number) {
    this.router.navigate(['system/users/edit/', id]);
  }
}
