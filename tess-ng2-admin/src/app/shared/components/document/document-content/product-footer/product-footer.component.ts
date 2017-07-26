import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'product-footer',
	templateUrl: 'product-footer.component.html',
	host: {
		class: 'row head no-gutters py-1'
	}
})

export class ProductFooterComponent implements OnInit {
	@Input() priceFixedTot: number = 0;
	@Input() priceVariableTot: number = 0;

	ngOnInit() { }
}