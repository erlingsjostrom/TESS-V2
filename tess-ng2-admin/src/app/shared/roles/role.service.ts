import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
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
  private url = Config.API_URL + 'AUTH/Roles';
  
  get(): Observable<IRole[]> {
    let headers = new Headers();
    headers.append('Accept', Config.API_HEADERS.Accept);
    return this._http.get(this.url, {
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
}