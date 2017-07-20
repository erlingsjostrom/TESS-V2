import { LoadingService } from '../shared/components/loading/loading.service';
import { LoadingComponent } from '../shared/components/loading/loading.component';
import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';

import { routing }       from './pages.routing';
import { NgaModule } from '../theme/nga.module';
import { AppTranslationModule } from '../app.translation.module';

import { Pages } from './pages.component';

@NgModule({
  imports: [CommonModule, AppTranslationModule, NgaModule, routing],
  declarations: [Pages, LoadingComponent],
  providers: [LoadingService]
})
export class PagesModule {
}
