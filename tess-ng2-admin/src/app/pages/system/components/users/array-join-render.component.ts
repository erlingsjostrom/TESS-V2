import { IRole } from '../../../../shared/users/user.service';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  template: `
    {{renderValue}}
  `,
})
export class ArrayJoinRenderComponent implements OnInit {
  renderValue: string;

  @Input() value: any[];
  @Input() rowData: any;
  
  ngOnInit() {
    this.renderValue = this.value.map(r => r.Name).join(", ");
  }
}