import { DocumentHeadComponent } from './document-head/document-head.component';
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: [
    DocumentHeadComponent
  ],
  exports: [
    DocumentHeadComponent
  ]
})
export class DocumentModule {
}
