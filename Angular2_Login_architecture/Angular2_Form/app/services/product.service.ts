import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';

import { Product } from '../models/product';

@Injectable()
export class ProductService {
    constructor(private http: Http) { }

    getAll() {
        var body = '';
     
        return this.http.get('http://localhost:8082/v1/Products/Product/all', this.jwt()).map(
            (response: Response) => response.json()
        );
    }

    getById(id: number) {
        return this.http.get('http://localhost:8082/v1/Products/Product/' + id, this.jwt()).map((response: Response) => response.json());
    }

    create(prd: Product) {
        return this.http.post('http://localhost:8082/v1/Products', prd, this.jwt()).map((response: Response) => response.json());
    }

    update(prd: Product) {
        return this.http.put('http://localhost:8082/v1/Products' + prd.id, prd, this.jwt()).map((response: Response) => response.json());
    }

    delete(id: number) {
        return this.http.delete('http://localhost:8082/v1/Products' + id, this.jwt()).map((response: Response) => response.json());
    }
    
    private jwt() {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && currentUser.token) {
            let headers = new Headers({ 'Token': currentUser.token });
            return new RequestOptions({ headers: headers });
        }
    }
}