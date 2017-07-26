import { Component, OnInit } from '@angular/core';

@Component({
	selector: 'product-item',
	templateUrl: 'product-item.component.html'
})

export class ProductItemComponent implements OnInit {
	
	item: ProductItem = {
		nr: 1010,
		description: "Insert very important content here",
		priceFixed: 500,
		priceVariable: 50
	}

	ngOnInit() { }
}

export interface ProductItem {
	nr: number,
	description: string,
	priceFixed: number,
	priceVariable: number
}