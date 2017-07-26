import { DocumentContentComponent } from './document-content/document-content.component';
import { DocumentHeadComponent } from './document-head/document-head.component';
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: [
    DocumentHeadComponent,
    DocumentContentComponent
  ],
  exports: [
    DocumentHeadComponent,
    DocumentContentComponent
  ]
})
export class DocumentModule {
}
