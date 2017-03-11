import { Component, OnInit } from '@angular/core';
import { User } from './models/user';
import { SharedService } from './services/index';

@Component({
    moduleId: module.id,
    selector: 'app',
    templateUrl: 'app.component.html'
})

export class AppComponent implements OnInit {
    public currentUser: User;
    menuopened: boolean;
    activeTab: string;
    mobilemenuopened: boolean;

    constructor(private sharedService: SharedService) {
        this.menuopened = false;
        this.activeTab = "H";
        this.mobilemenuopened = false;
    }

    ngOnInit() {
        this.currentUser = this.sharedService.globalUser;
    }

    getHeader() {
        if (this.sharedService.globalUser == null)
            return false;
        else
            return true;
    }

    openmobilemenu() {
        this.mobilemenuopened = !this.mobilemenuopened;
    }

    openmenu() {
        this.menuopened = !this.menuopened;
    }

    setActiveTab(tabId: string) {
        this.activeTab = tabId;
    }

}