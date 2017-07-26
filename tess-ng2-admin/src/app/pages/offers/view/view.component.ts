import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";

let jsPDF = jspdf;
let _html2pdf = html2pdf;

@Component({
	selector: 'view',
	templateUrl: 'view.component.html',
	styleUrls: ['./view.component.scss']
})

export class ViewComponent implements OnInit {
	@ViewChild('pdfPage') pdfPage: ElementRef;
	title: string = "View"
	constructor(
		private _route: ActivatedRoute,
		private _router: Router
	) {}

	ngOnInit() { 
		const id = this._route.snapshot.paramMap.get('id');
	}

	createPdf () {
	
		var style = window.getComputedStyle(this.pdfPage.nativeElement);
		console.log(style.cssText);
	}
}