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
    console.log(this._router.url);
    this._state.subscribe('menu.activeLink', (activeLink) => {
      if (activeLink) {
        this.activePageTitle = activeLink.title;
      }

      // var bc = this.breadcrumbs;
      // this._router.events.subscribe((evt) => {
      //     if (evt instanceof NavigationEnd) {
      //         var url = evt.url;
      //         if (url === '' || url === '/') {
      //             bc.length = 0;
      //         } else {
      //             bc.push(evt.url.substr(1));
      //         }
      //     }
      // });

    });
  }
}


