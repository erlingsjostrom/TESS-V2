import { TestBed, inject } from '@angular/core/testing';

import { RadiobuttonComponent } from './radiobutton.component';

describe('a radiobutton component', () => {
	let component: RadiobuttonComponent;

	// register all needed dependencies
	beforeEach(() => {
		TestBed.configureTestingModule({
			providers: [
				RadiobuttonComponent
			]
		});
	});

	// instantiation through framework injection
	beforeEach(inject([RadiobuttonComponent], (RadiobuttonComponent) => {
		component = RadiobuttonComponent;
	}));

	it('should have an instance', () => {
		expect(component).toBeDefined();
	});
});