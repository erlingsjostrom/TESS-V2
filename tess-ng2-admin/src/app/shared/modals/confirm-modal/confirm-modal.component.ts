import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'add-service-modal',
  styleUrls: [('./confirm-modal.component.scss')],
  templateUrl: './confirm-modal.component.html',
  encapsulation: ViewEncapsulation.None,
})

export class ConfirmModal implements OnInit {

  modalHeader: string;
  modalContent: string = '';
  modalCloseButtonText: string = '';
  modalContinueButtonText: string = '';
  modalContinueButtonCallback: () => void = () => {return};
  constructor(private activeModal: NgbActiveModal) {
  }

  ngOnInit() {}

  closeModal () {
    this.activeModal.close();
  }
  continueAction () {
    this.modalContinueButtonCallback();
    this.activeModal.close();
  }
}
