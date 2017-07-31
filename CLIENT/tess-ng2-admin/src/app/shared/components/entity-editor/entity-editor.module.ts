import { TinymceModule } from 'angular2-tinymce';
import { FormsModule } from '@angular/forms';
import { LoadingService } from '../loading/loading.service';
import { LoadingComponent } from '../loading/loading.component';
import { EntityEditorComponent } from './entity-editor.component';
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import 'tinymce/plugins/fullscreen/plugin.js';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    TinymceModule.withConfig({
      plugins: [
    		'link', 
    		'paste', 
    		'table', 
    		'advlist', 
    		'autoresize', 
    		'lists',
    		'code',
        'fullscreen'
    	],
      toolbar: "undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | fullscreen"
    })
  ],
  declarations: [
    EntityEditorComponent,
    LoadingComponent,
  ],
  providers: [
    LoadingService
  ],
  exports: [
    EntityEditorComponent
  ]
})
export class EntityEditorModule {
}
