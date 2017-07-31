import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Rx';

@Injectable()
export class LoadingService {
  public isLoading: boolean;

  show() {
    this.isLoading = true;
  }
  
  hide() {
    this.isLoading = false;
  }
}