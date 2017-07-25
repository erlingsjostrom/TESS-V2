import { DataTableModule } from 'primeng/primeng';
import { CustomerService } from 'app/shared/resources/customers/customer.service';
import { AllComponent } from './all/all.component';
import { EditComponent } from './edit/edit.component';
import { NgModule }      from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgaModule } from 'app/theme/nga.module';
import { Customers } from './customers.component';
import { routing } from './customers.routing';
import { EntityTableModule } from "app/shared/components/entity-table/entity-table.module";
import { ModalModule } from "app/shared/modals/modal.module";
import { EntityEditorModule } from "app/shared/components/entity-editor/entity-editor.module";

const COMPONENTS = [
  AllComponent,
  EditComponent,
]

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgaModule,
    routing,
    ModalModule,
    EntityTableModule,
    EntityEditorModule
  ],
  declarations: [
    Customers,
    ... COMPONENTS
  ],
  providers: [
    CustomerService,
  ]
})
export class CustomersModule {
}
