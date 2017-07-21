import { BaseService } from '../service';
import { inherits } from 'util';
import { Inject, Injectable } from '@angular/core';
import { Http, Response, Headers, Request, RequestMethod } from '@angular/http';
import { Observable } from 'rxjs/Rx';

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
export class UserService extends BaseService{
  constructor (@Inject(Http) _http: Http) {
    super(_http);
    this.serviceURL = 'AUTH/Users';
  }

  getAll(): Observable<Response> {
    return this._request(this.getUrlWithRoles(), { method: RequestMethod.Get });
  }
  
  get(id: number): Observable<Response> {
    return this._request(this.getUrlWithRoles(id), { method: RequestMethod.Get });
  }

  put(user: IUser): Observable<Response> {
    return this._request(this.getUrl(user.Id), { method: RequestMethod.Put }, user);
  }

  putRole(user: IUser, role: IRole): Observable<Response> {
    return this._request(this.getUrl(user.Id), { method: RequestMethod.Put }, role);
  }

  post(user: IUser): Observable<Response> {
    return this._request(this.getUrlWithRoles(), { method: RequestMethod.Post }, user);
  }

  delete(user: IUser): Observable<Response> {
    return this._request(this.getUrl(user.Id), { method: RequestMethod.Delete });
  }

  private getUrlWithRoles(id?: number): string {
    let url = this.getUrl(id);
    url += '?$expand=Roles';
    
    return url;
  }
}