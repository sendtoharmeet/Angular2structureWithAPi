import { Injectable } from '@angular/core';

@Injectable()
export class SharedService {
    public globalUser = JSON.parse(localStorage.getItem('currentUser'));
}