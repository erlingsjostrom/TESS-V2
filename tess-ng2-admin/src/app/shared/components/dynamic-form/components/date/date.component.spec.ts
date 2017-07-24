import { TestBed, inject } from '@angular/core/testing';

import { DateComponent } from './date.component';

describe('a date component', () => {
	let component: DateComponent;

	// register all needed dependencies
	beforeEach(() => {
		TestBed.configureTestingModule({
			providers: [
				DateComponent
			]
		});
	});

	// instantiation through framework injection
	beforeEach(inject([DateComponent], (DateComponent) => {
		component = DateComponent;
	}));

	it('should have an instance', () => {
		expect(component).toBeDefined();
	});
});