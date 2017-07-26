import {Component} from '@angular/core';

import { GlobalState } from '../../../global.state';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'ba-content-top',
  styleUrls: ['./baContentTop.scss'],
  templateUrl: './baContentTop.html',
})
export class BaContentTop {

  public activePageTitle:string = '';
  public breadcrumbs: string[] = [];

  constructor(
    private _state: GlobalState,
    private _router: Router
  ) {
    this._state.subscribe('menu.activeLink', (activeLink) => {
      if (activeLink) {
        this.activePageTitle = activeLink.title;
      }
    });
  }
}


