import { Component, ViewEncapsulation } from '@angular/core';
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
    loading: true
  }

  content: DataTableList<IUser> = new DataTableList<IUser>();
  
  constructor(
    private userService: UserService,
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
      console.log("Modified row: ", event.index);
    }, 800)
  }

  onEditComplete(event){
    clearTimeout(this.editTimeout);
    this.state.edited = this.content.isModified();
    console.log("Modified Id: ", event.data.Id);
  }

}
export interface Identifiable {
  Id: number
  test
}
export class DataTableList<Identifiable> {
  data: Identifiable[] = [];
  private rawData: Identifiable[] = [];
  
  load(data: Identifiable[]) {
    this.data = data;
    this.rawData = this.cloneData(data);
  }

  modified(ids?: number[]): Identifiable[] {
    if (ids) {
      const modifies = this.data.filter((item) => item.test )
    } else {
      return this.data.filter((item, index: number) => {
        return !(JSON.stringify(item) === JSON.stringify(this.rawData[index]));
      });
    }
  }

  isModified(ids?: number[]): boolean {
    return this.modified(id).length > 0;
  }
  

  private cloneData(data: Identifiable[]): Identifiable[] {
    let clonedData: Identifiable[] = [];
    data.forEach(item => {
      clonedData.push(Object.assign({}, JSON.parse(JSON.stringify(item))));
    });
    return clonedData;
  }
}