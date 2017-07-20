import { Observable, Subject } from 'rxjs/Rx';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'add-service-modal',
  styleUrls: [('./confirm-modal.component.scss')],
  templateUrl: './confirm-modal.component.html',
  encapsulation: ViewEncapsulation.None,
})

export class ConfirmModal implements OnInit {
  modalSubject: Subject<boolean> = new Subject<boolean>();
  modalHeader: string;
  modalContent: string = '';
  modalCloseButtonText: string = '';
  modalContinueButtonText: string = '';
  modalCloseModalClass: string = '';
  modalContinueButtonClass: string = '';
  
  constructor(private activeModal: NgbActiveModal) {
  }

  ngOnInit() {}

  closeModal () {
    this.modalSubject.next(false);
    this.activeModal.close();
  }

  continueAction () {
    this.modalSubject.next(true);
    this.activeModal.close();
  }
}
