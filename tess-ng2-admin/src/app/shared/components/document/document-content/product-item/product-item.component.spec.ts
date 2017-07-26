import { TestBed, inject } from '@angular/core/testing';

import { ProductItemComponent } from './product-item.component';

describe('a product-item component', () => {
	let component: ProductItemComponent;

	// register all needed dependencies
	beforeEach(() => {
		TestBed.configureTestingModule({
			providers: [
				ProductItemComponent
			]
		});
	});

	// instantiation through framework injection
	beforeEach(inject([ProductItemComponent], (ProductItemComponent) => {
		component = ProductItemComponent;
	}));

	it('should have an instance', () => {
		expect(component).toBeDefined();
	});
});