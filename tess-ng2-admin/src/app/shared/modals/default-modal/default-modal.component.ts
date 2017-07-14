import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'add-service-modal',
  styleUrls: [('./default-modal.component.scss')],
  templateUrl: './default-modal.component.html',
  encapsulation: ViewEncapsulation.None,
})

export class DefaultModal implements OnInit {

  modalHeader: string;
  modalContent: string = '';
  modalButtonText: string = '';

  constructor(private activeModal: NgbActiveModal) {
  }

  ngOnInit() {}

  closeModal() {
    this.activeModal.close();
  }
}
