import { BaseService } from '../service';
import { Inject, Injectable } from '@angular/core';
import { Http, Response, Headers, Request, RequestMethod } from '@angular/http';
import { Observable } from 'rxjs/Rx';

// Import RxJs required methods
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/timeout';

export interface ICustomer {
  Id: number,
  Name: string,
  Type: string,
  CorporateIdentityNumber: string,
}

@Injectable()
export class CustomerService extends BaseService {
  constructor (@Inject(Http) _http: Http) {
    super(_http);
    this.serviceURL = 'DB1/Customers';
  }

  post(customer: ICustomer): Observable<Response> {
    return this._request(this.getUrl(), { method: RequestMethod.Post }, customer);
  }

  put(customer: ICustomer): Observable<Response> {
    return this._request(this.getUrl(customer.Id),  { method: RequestMethod.Put }, customer);
  }

  delete(customer: ICustomer): Observable<Response> {
    return this._request(this.getUrl(customer.Id), { method: RequestMethod.Delete });
  }
  
  // protected getUrl(id?: number): string {
  //   return super.getUrl(id) + "?$expand=Customer";
  // }
  
}