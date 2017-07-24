import { NgModule }      from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgaModule } from 'app/theme/nga.module';
import { System } from './system.component';
import { routing } from './system.routing';

import { UsersModule } from './users/users.module';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgaModule,
    routing,
    UsersModule
  ],
  declarations: [
    System,
  ],
})
export class SystemModule {
}
