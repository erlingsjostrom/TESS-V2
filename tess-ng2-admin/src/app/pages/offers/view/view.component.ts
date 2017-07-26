import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";

let _html2pdf = html2pdf;

@Component({
	selector: 'view',
	templateUrl: 'view.component.html',
	styleUrls: ['./view.component.scss']
})

export class ViewComponent implements OnInit {
	@ViewChild('pdfPage') pdfPage: ElementRef;
	title: string = "View"
	private _id: number;

	constructor(
		private _route: ActivatedRoute,
		private _router: Router
	) {}

	ngOnInit() { 
		this._id = +this._route.snapshot.paramMap.get('id');
		(<any>window).displayPixelRatio = 2;
	}

	createPdf () {
		html2pdf(this.pdfPage.nativeElement, {
		  margin:       [19, 12, 19, 10],
		  filename:     'offer' + this._id + '.pdf',
		  image:        { type: 'png' },
		  html2canvas:  { dpi: 200, letterRendering: true, onrendered: this.onRendered },
		});
	}

	onRendered() {
		console.log("pdf rendered");
	}
}