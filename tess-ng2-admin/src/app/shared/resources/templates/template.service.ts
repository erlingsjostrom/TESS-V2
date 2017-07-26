import { BaseService } from '../service';
import { Inject, Injectable } from '@angular/core';
import { Http, Response, Headers, Request, RequestMethod } from '@angular/http';
import { Observable } from 'rxjs/Rx';

// Import RxJs required methods
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/timeout';

export interface ITemplate {
  Id: number,
  Name: string,
  Description: string,
  EntityType: string,
}

@Injectable()
export class TemplateService extends BaseService {
  constructor (@Inject(Http) _http: Http) {
    super(_http);
    this.serviceURL = 'DB1/Templates';
  }

  post(template: ITemplate): Observable<Response> {
    return this._request(this.getUrl(), { method: RequestMethod.Post }, template);
  }

  put(template: ITemplate): Observable<Response> {
    return this._request(this.getUrl(template.Id),  { method: RequestMethod.Put }, template);
  }

  delete(template: ITemplate): Observable<Response> {
    return this._request(this.getUrl(template.Id), { method: RequestMethod.Delete });
  }
}