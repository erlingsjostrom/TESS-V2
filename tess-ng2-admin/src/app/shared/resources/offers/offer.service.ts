import { BaseService } from '../service';
import { Inject, Injectable } from '@angular/core';
import { Http, Response, Headers, Request, RequestMethod } from '@angular/http';
import { Observable } from 'rxjs/Rx';

// Import RxJs required methods
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/timeout';

export interface IOffer {
  Id: number,
  Status: string,
  Title: string
}

@Injectable()
export class OfferService extends BaseService {
  constructor (@Inject(Http) _http: Http) {
    super(_http);
    this.serviceURL = 'DB1/Offers';
  }

  post(offer: IOffer): Observable<Response> {
    return this._request(this.getUrl(), { method: RequestMethod.Post }, offer);
  }

  put(offer: IOffer): Observable<Response> {
    return this._request(this.getUrl(offer.Id),  { method: RequestMethod.Put }, offer);
  }

  delete(offer: IOffer): Observable<Response> {
    return this._request(this.getUrl(offer.Id), { method: RequestMethod.Delete });
  }
  
  protected getUrl(id?: number): string {
    return super.getUrl(id) + "?$expand=Customer";
  }
  
}