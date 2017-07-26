import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'product-head',
	templateUrl: 'product-head.component.html',
	host: {
		class: 'row head no-gutters py-1'
	}
})

export class ProductHeadComponent implements OnInit {
	@Input() title:string = "";
	@Input() priceTypeFixed:string = "";
	@Input() priceTypeVariable:string = "";
	ngOnInit() { }
}