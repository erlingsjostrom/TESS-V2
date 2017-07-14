import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DefaultModal } from './default-modal/default-modal.component';
import { ConfirmModal } from './confirm-modal/confirm-modal.component';
@Injectable()
export class ModalService {
  constructor(private ngbModalService: NgbModal) {}

  showInfoModal(
    title:string, 
    text:string, 
    buttonText?: string, 
    size?: ModalSize, 
    isStatic?: boolean
  ) {
    const modalButtonText = buttonText ? buttonText : 'Ok';
    const settings = this.parseSettings(size, isStatic);
    const activeModal = this.ngbModalService.open(DefaultModal, settings);

    activeModal.componentInstance.modalHeader = title;
    activeModal.componentInstance.modalContent = text;
    activeModal.componentInstance.modalButtonText = modalButtonText;
  }

  showConfirmModal(
    title:string, 
    text:string, 
    continueButtonCallback: () => void,
    continueButtonText?: string, 
    closeButtonText?: string,
    size?: ModalSize, 
    isStatic?: boolean
  ) {
    const modalContinueButtonText = continueButtonText ? continueButtonText : 'Continue';
    const modalCloseButtonText = closeButtonText ? closeButtonText : 'Close';
    const settings = this.parseSettings(size, isStatic);
    const activeModal = this.ngbModalService.open(ConfirmModal, settings);

    activeModal.componentInstance.modalHeader = title;
    activeModal.componentInstance.modalContent = text;
    activeModal.componentInstance.modalContinueButtonCallback = continueButtonCallback;
    activeModal.componentInstance.modalContinueButtonText = modalContinueButtonText;
    activeModal.componentInstance.modalCloseButtonText = modalCloseButtonText;
  }

  private parseSettings(size?: ModalSize, isStatic?: boolean) {
    let modalBackdrop: any = isStatic ? 'static' : undefined;
    let modalSize: 'lg' | 'sm';
    switch(size) {
      case ModalSize.Small: 
        modalSize = 'sm';
        break;
      case ModalSize.Large: 
        modalSize = 'lg';
        break;
      default: 
        modalSize = 'lg';
        break;
    }
    return {
      size: modalSize,
      backdrop: modalBackdrop
    }
  }
}

export enum ModalType {
  Default,
  Confirm
}
export enum ModalSize {
  Small,
  Large,
}