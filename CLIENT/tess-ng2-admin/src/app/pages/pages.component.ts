import { Component } from '@angular/core';
import { Routes } from '@angular/router';

import { BaMenuService } from '../theme';
import { PAGES_MENU } from './pages.menu';

@Component({
  selector: 'pages',
  template: `
    <div class="hidden-print">
       <ba-sidebar></ba-sidebar>
        <ba-page-top></ba-page-top>
    </div>
    <div class="al-main">
      <div class="al-content">
        <breadcrumb></breadcrumb>
        <!--<ba-content-top></ba-content-top>-->
        <router-outlet></router-outlet>
      </div>
    </div>
    <div class="hidden-print">
      <footer class="al-footer clearfix">
        <div class="al-footer-right" translate>{{'general.created_with'}} <i class="ion-heart"></i> and 🍺</div>
        <div class="al-footer-main clearfix">
          <div class="al-copy">&copy; <a href="http://tieto.com" translate>Tieto</a> 2016</div>
        </div>
      </footer>
      <ba-back-top position="200"></ba-back-top>
    </div>
    
    `
})
export class Pages {

  constructor(private _menuService: BaMenuService,) {
  }

  ngOnInit() {
    this._menuService.updateMenuByRoutes(<Routes>PAGES_MENU);
  }
}
