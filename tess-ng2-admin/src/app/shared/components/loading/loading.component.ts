import { Component, OnInit, Input } from '@angular/core';

import { LoadingService } from './loading.service';

@Component({
	selector: 'loading',
	templateUrl: 'loading.component.html'
})

export class LoadingComponent implements OnInit {
	@Input()
	public loading:boolean = false;
	constructor(
		private loadingService: LoadingService,
	) {
		this.loading = loadingService.isLoading;
	}
	ngOnInit() { }
}