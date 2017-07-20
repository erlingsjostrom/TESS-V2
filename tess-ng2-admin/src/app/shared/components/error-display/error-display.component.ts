import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'error',
	templateUrl: 'error.component.html'
})

export class LoadingComponent implements OnInit {
    errorText = "";

    constructor(
		errorText: string,
	) {
		this.errorText = "Error";
    }
    
	ngOnInit() {
    }
}