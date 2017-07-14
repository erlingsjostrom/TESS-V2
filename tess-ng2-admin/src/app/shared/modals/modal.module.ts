import { ConfirmModal } from './confirm-modal/confirm-modal.component';
import { DefaultModal } from './default-modal/default-modal.component';
import { ModalService } from './modal.service';

import { NgModule } from '@angular/core';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
   NgbModalModule,
  ],
  declarations: [
    DefaultModal,
    ConfirmModal,
  ],
  entryComponents: [
    DefaultModal,
    ConfirmModal,
  ],
  providers: [
    ModalService
  ]
})
export class ModalModule {
}