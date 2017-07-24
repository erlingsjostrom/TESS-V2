import { BaseService } from '../service';
import { Inject, Injectable } from '@angular/core';
import { Headers, Http, RequestMethod, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';

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
export class RoleService extends BaseService {
  constructor (@Inject(Http) _http: Http) {
    super(_http);
    this.serviceURL = 'AUTH/Roles';
  }
  
}