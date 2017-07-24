import { TestBed, inject } from '@angular/core/testing';

import { RichtextComponent } from './richtext.component';

describe('a richtext component', () => {
	let component: RichtextComponent;

	// register all needed dependencies
	beforeEach(() => {
		TestBed.configureTestingModule({
			providers: [
				RichtextComponent
			]
		});
	});

	// instantiation through framework injection
	beforeEach(inject([RichtextComponent], (RichtextComponent) => {
		component = RichtextComponent;
	}));

	it('should have an instance', () => {
		expect(component).toBeDefined();
	});
});