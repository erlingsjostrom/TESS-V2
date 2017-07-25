import { Observable } from 'rxjs/Rx';
import { Http, Headers, RequestOptionsArgs, Response, RequestMethod } from '@angular/http';
import { Config } from 'app/shared/config';

export abstract class BaseService {
    constructor(protected _http: Http){
    }

    protected serviceURL: string;

    getAll(): Observable<Response> {
        return this._request(this.getUrl(), { method: RequestMethod.Get });
    }
    
    get(id: number): Observable<Response> {
        return this._request(this.getUrl(id), { method: RequestMethod.Get });
    }

    protected _request(url: string, options?: RequestOptionsArgs, data?: Object): Observable<Response> {
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

    protected getUrl(id?: number): string {
    let url = Config.API_URL + this.serviceURL;
    if (id) {
      url += '(' + id + ')';
    }
    return url;
  }
}