import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'product-footer',
	templateUrl: 'product-footer.component.html',
	host: {
		class: 'row footer no-gutters py-1 mb-2'
	}
})

export class ProductFooterComponent implements OnInit {
	@Input() priceFixedTot: number = 0;
	@Input() priceVariableTot: number = 0;

	ngOnInit() { }
}