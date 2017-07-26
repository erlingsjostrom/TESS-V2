import { ProductFooterComponent } from './document-content/product-footer/product-footer.component';
import { ProductHeadComponent } from './document-content/product-head/product-head.component';
import { ProductItemComponent } from './document-content/product-item/product-item.component';
import { MoneyFormatPipe } from '../../pipes/money-format-pipe';
import { DocumentContentComponent } from './document-content/document-content.component';
import { DocumentHeadComponent } from './document-head/document-head.component';
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

const PIPES = [
  MoneyFormatPipe
];

const COMPONENTS = [
  DocumentHeadComponent,
  DocumentContentComponent,
  ProductItemComponent,
  ProductHeadComponent,
  ProductFooterComponent
]

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: [
    ... PIPES,
    ... COMPONENTS,
  ],
  exports: [
    ... COMPONENTS,
  ]
})
export class DocumentModule {
}
