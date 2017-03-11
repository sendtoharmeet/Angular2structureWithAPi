import { Component, ElementRef, ViewChild, OnInit } from '@angular/core';

import { Product } from '../models/product';
import { ProductService } from '../services/index';

@Component({
    moduleId: module.id,
    templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {
    products: Product[] = [];
    
    constructor(private productService: ProductService, myElement: ElementRef) {
    }

    ngOnInit() {
        this.loadProducts();
    }

    deleteProduct(id: number) {
        this.productService.delete(id).subscribe(() => { this.loadProducts() });
    }

    private loadProducts() {
        this.productService.getAll().subscribe(prds => {
            this.products = prds;
        });
    }
}