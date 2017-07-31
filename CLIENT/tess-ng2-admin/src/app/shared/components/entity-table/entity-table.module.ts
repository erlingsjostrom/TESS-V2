import { CapitalizePipe } from 'app/shared/pipes/capitalize.pipe';
import { ArrayMapJoinPipe } from 'app/shared/pipes/array-map-join.pipe';
import { ArraySortPipe } from 'app/shared/pipes/array-sort.pipe';
import { EntityTableComponent } from './entity-table.component';
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from '@angular/forms';
import { DataTableModule } from "primeng/primeng";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    DataTableModule,
  ],
  declarations: [
    EntityTableComponent,
    ArraySortPipe,
    ArrayMapJoinPipe,
    CapitalizePipe
  ],
  providers: [

  ],
  exports: [
    EntityTableComponent
  ]
})
export class EntityTableModule {
}
