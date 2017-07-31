import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';

import { routing }       from './pages.routing';
import { NgaModule } from '../theme/nga.module';
import { AppTranslationModule } from '../app.translation.module';
import { BreadcrumbModule } from '../shared/breadcrumb/breadcrumb.module';
import { Pages } from './pages.component';

@NgModule({
  imports: [
    CommonModule, 
    AppTranslationModule,
    NgaModule, 
    routing,
    BreadcrumbModule
  ],
  declarations: [
    Pages, 
  ]
})
export class PagesModule {
}
