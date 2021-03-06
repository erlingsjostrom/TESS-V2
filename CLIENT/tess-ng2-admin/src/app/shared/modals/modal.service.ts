import { Observable } from 'rxjs/Rx';
import { Component, Injectable } from '@angular/core';
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
    isStatic?: boolean,
    positionTop?: boolean
  ) {
    const modalButtonText = buttonText ? buttonText : 'Ok';
    let settings = this.parseSettings(size, isStatic, positionTop);
    const activeModal = this.ngbModalService.open(DefaultModal, settings);

    activeModal.componentInstance.modalHeader = title;
    activeModal.componentInstance.modalContent = text;
    activeModal.componentInstance.modalButtonText = modalButtonText;
  }

  showConfirmModal(
    title:string, 
    text:string, 
    continueButtonText?: string, 
    closeButtonText?: string,
    continueButtonClass?: string,
    closeButtonClass?: string,
    size?: ModalSize, 
    isStatic?: boolean,
    positionTop?: boolean
  ): Observable<boolean> {
    const modalContinueButtonText = continueButtonText ? continueButtonText : 'Continue';
    const modalCloseButtonText = closeButtonText ? closeButtonText : 'Close';
    const modalContinueButtonClass = continueButtonClass ? continueButtonClass : 'btn-primary-dark';
    const modalCloseButtonClass = closeButtonClass ? closeButtonClass : '';
    const settings = this.parseSettings(size, isStatic, positionTop);
    const activeModal = this.ngbModalService.open(ConfirmModal, settings);

    activeModal.componentInstance.modalHeader = title;
    activeModal.componentInstance.modalContent = text;
    activeModal.componentInstance.modalContinueButtonText = modalContinueButtonText;
    activeModal.componentInstance.modalCloseButtonText = modalCloseButtonText;
    activeModal.componentInstance.modalContinueButtonClass = modalContinueButtonClass;
    activeModal.componentInstance.modalCloseButtonClass = modalCloseButtonClass;
    
    return activeModal.componentInstance.modalSubject;
  }

  showCustomModal(
    modalComponent: Component,
    data: any,
    size?: ModalSize, 
    isStatic?: boolean
  ) {
    const settings = this.parseSettings(size, isStatic);
    const activeModal = this.ngbModalService.open(modalComponent, settings);
    activeModal.componentInstance.setData(data);
  }

  private parseSettings(size?: ModalSize, isStatic?: boolean, positionTop?: boolean) {
    const modalBackdrop: any = isStatic ? 'static' : undefined;
    const windowClass = positionTop ? "" : "alert-modal";
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
      backdrop: modalBackdrop,
      windowClass
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