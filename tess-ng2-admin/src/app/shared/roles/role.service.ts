import { Injectable } from '@angular/core';
import { Headers, Http, RequestMethod, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { Config } from 'app/shared/config';

// Import RxJs required methods
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/timeout';

export interface IRole {
  Id: number,
  Name: string,
  Description: string,
}

@Injectable()
export class RoleService {
  constructor (private _http: Http) {}
  
  get(): Observable<Response> {
    return this._request(this.getUrl(), { method: RequestMethod.Get });
  }

  private _request(url: string, options?: RequestOptionsArgs, data?: Object): Observable<Response> {
    let headers = new Headers();
    headers.append('Accept', Config.API_HEADERS.Accept);
    headers.append('Content-Type', 'application/json;charset=UTF-8');
    options.withCredentials = true;
    options.headers = headers;

    if (data) {
      options.body = JSON.stringify(data);
    }

    return this._http.request(url, options)
              .timeout(8000)
              .retry(3)
              .map((response: Response) => {
                return response;
              })
              .catch((error: any) => {
                return Observable.throw(error);
              });
  }

  private getUrl(id?: number): string {
    let url = Config.API_URL + 'AUTH/Roles';
    if (id) {
      url += '(' + id + ')';
    }
    return url;
  }
}