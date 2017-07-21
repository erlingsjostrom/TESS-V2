import { Injectable } from '@angular/core';
import { Http, Response, Headers, Request, RequestOptionsArgs, RequestMethod } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { Config } from 'app/shared/config';

// Import RxJs required methods
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/timeout';

export interface IUser {
  Id: number,
  Name: string,
  WindowsUser: string,
  Roles: IRole[]
}
export interface IRole {
  Id: number,
  Name: string,
  Description: string,
}

@Injectable()
export class UserService {
  constructor (private _http: Http) {}

  getAll(): Observable<Response> {
    return this._request(this.getUrl(), { method: RequestMethod.Get });
  }
  
  get(id: number): Observable<Response> {
    return this._request(this.getUrl(id), { method: RequestMethod.Get });
  }

  put(user: IUser): Observable<Response> {
    return this._request(this.getUrl(user.Id, true), { method: RequestMethod.Put }, user);
  }

  putRole(user: IUser, role: IRole): Observable<Response> {
    return this._request(this.getUrl(user.Id, true), { method: RequestMethod.Put }, role);
  }

  post(user: IUser): Observable<Response> {
    return this._request(this.getUrl(), { method: RequestMethod.Post }, user);
  }

  delete(user: IUser): Observable<Response> {
    return this._request(this.getUrl(user.Id, true), { method: RequestMethod.Delete });
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

  private getUrl(id?: number, excludeSuffix?: boolean): string {
    let url = Config.API_URL + 'AUTH/Users';
    if (id) {
      url += '(' + id + ')';
    }
    if(!excludeSuffix) {
      url += '?$expand=Roles';
    }
    return url;
  }
}