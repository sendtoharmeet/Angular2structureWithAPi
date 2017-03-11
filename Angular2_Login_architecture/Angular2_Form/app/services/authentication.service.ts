import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class AuthenticationService {
    constructor(private http: Http) { }

    login(username: string, password: string) {
        var body = '';
        var headers = this.jwt(username, password);
       
        return this.http.post('http://localhost:8082/v1/user/login', body, headers)
            .map((response: Response) => {
                console.log(response);
                let user = response.json();
                if (response.headers.get("Token") && response.headers.get("Token") != "" && response.headers.get("Token") != null) {
                    var currentUser: { username: string, token: string, tokenexpiry: string } = {
                        username: username,
                        token: response.headers.get("Token"),
                        tokenexpiry: response.headers.get("TokenExpiry")
                    };

                    localStorage.setItem('currentUser', JSON.stringify(currentUser));
                }
            }).catch(
                this.handleError
            );
    }

    logout() {
        localStorage.removeItem('currentUser');
    }

    private handleError(error: Response | any) {
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Promise.reject(errMsg);
    }

    private jwt(usr: string, pwd: string) {
        let headers = new Headers({ 'Authorization': 'Basic ' + btoa(usr + ':' + pwd) });
        return new RequestOptions({ headers: headers });
    }
}