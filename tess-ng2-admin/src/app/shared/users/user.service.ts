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

  getAll(): Observable<IUser[]> {
    let headers = new Headers();
    headers.append('Accept', Config.API_HEADERS.Accept);
    return this._http.get(this.getUrl(), {
                withCredentials: true,
                headers: headers
              })
              .timeout(5000)
              .map((response: Response) => {
                return response.json().value;
              })
              .catch((error: any) => {
                return Observable.throw(error);
              });
  }
  
  get(id: number): Observable<IUser> {
    let headers = new Headers();
    headers.append('Accept', Config.API_HEADERS.Accept);
    return this._http.get(this.getUrl(id), {
                withCredentials: true,
                headers: headers
              })
              .timeout(5000)
              .map((response: Response) => {
                return response.json();
              })
              .catch((error: any) => {
                return Observable.throw(error);
              });
  }

  put(user: IUser): Observable<Response> {
    return this._request(this.getUrl(user.Id, true),  { method: RequestMethod.Put }, user);
  }

  putRole(user: IUser, role: IRole): Observable<Response> {
    return this._request(this.getUrl(user.Id, true), { method: RequestMethod.Put }, role)      
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