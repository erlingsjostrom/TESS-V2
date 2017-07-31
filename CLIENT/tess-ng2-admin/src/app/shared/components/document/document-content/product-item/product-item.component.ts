import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'product-item',
	templateUrl: 'product-item.component.html',
	host: {
		class: 'col-12 row no-gutters'
	}
})

export class ProductItemComponent implements OnInit {
	
	@Input() item: ProductItem = {
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