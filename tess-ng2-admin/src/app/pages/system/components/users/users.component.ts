import { Component, ViewEncapsulation } from '@angular/core';
import { ModalService, ModalSize, ModalType } from '../../../../shared/modals/modal.service';
import { IUser, UserService } from '../../../../shared/users/user.service';


@Component({
  selector: 'users-component',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
  encapsulation: ViewEncapsulation.None,
})

export class Users {
  state = {
    edited: false,
    loading: true,
    editMode: false,
  }

  content: DataTableList<IUser> = new DataTableList<IUser>();
  
  constructor(
    private userService: UserService,
    private modalService: ModalService
  ) {
    this.userService.get().subscribe((users) => {
      this.content.load(users);
      this.state.loading = false;
    })
  }
  
  private editTimeout;
  private editedIds: number[] = [];

  onEdit(event){
    clearTimeout(this.editTimeout);
    this.editTimeout = setTimeout(() => {
      this.editedIds.push(event.Id);
      this.state.edited = this.content.isModified();
    }, 800)
  }

  onEditComplete(event){
    clearTimeout(this.editTimeout);
    this.state.edited = this.content.isModified();
  }

  toggleEditMode() {
    if (this.state.edited){
      this.modalService.showConfirmModal(
        'Undo changes?', 
        'Edited content exists, do you really want to undo these changes?', 
        () => console.log("YEY CONTINUE!!"),
        'Undo changes',
        'Keep changes',
        ModalSize.Large
      );
      return;
    } 
    this.state.editMode = !this.state.editMode;
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