import { ArrayMapJoinPipe } from '../../pipes/array-map-join.pipe';
import { ArraySortPipe } from '../../pipes/array-sort.pipe';
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
    ArrayMapJoinPipe
  ],
  providers: [

  ],
  exports: [
    EntityTableComponent
  ]
})
export class EntityTableModule {
}
