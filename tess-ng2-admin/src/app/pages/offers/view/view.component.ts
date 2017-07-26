import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { ProductItem } from "app/shared/components/document/document-content/product-item/product-item.component";

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
	
	
	texts: string[] = [
		`<p>Fair trade cup galão, cinnamon grounds americano coffee beans espresso arabica. Aftertaste, mazagran, aftertaste cup beans siphon dark sugar. Wings, extra, seasonal coffee espresso black shop caramelization. Single origin sugar eu chicory cortado aroma extra redeye.</p>

		 <p>Roast black caffeine mocha body white to go. Milk and white organic caramelization kopi-luwak eu medium caramelization. Fair trade eu, milk that plunger pot, aged at con panna ut black breve. Doppio spoon frappuccino, foam as kopi-luwak sweet caffeine.</p>`,

		 `<p>Milk eu mug kopi-luwak cinnamon con panna strong. Caffeine, beans flavour eu aroma breve cortado that to go cinnamon. Foam id a cinnamon instant con panna trifecta. Roast dark, sweet french press redeye mug caffeine.</p>

		 <p>Grounds arabica mug plunger pot siphon froth fair trade extra frappuccino. Skinny, filter ut so dark single origin, brewed grounds coffee seasonal café au lait. Americano, turkish french press cup barista spoon seasonal lungo. Brewed, barista aged qui robust french press affogato mocha.</p>`,
	]

	items: ProductItem[] = [
		{
			nr: 1010,
			description: "Insert very important content here",
			priceFixed: 500,
			priceVariable: 50
		},
		{
			nr: 1010,
			description: "Insert very important content here",
			priceFixed: 500,
			priceVariable: 50
		},
		{
			nr: 1010,
			description: "Insert very important content here",
			priceFixed: 500,
			priceVariable: 50
		},
		{
			nr: 1010,
			description: "Insert very important content here",
			priceFixed: 500,
			priceVariable: 50
		},
		{
			nr: 1010,
			description: "Insert very important content here",
			priceFixed: 500,
			priceVariable: 50
		},
	]

	content: Content[] = [
		{
			type: "text",
			value: this.texts
		},
		{
			type: "product",
			value: this.items
		},
	]

}

export interface Content {
	type: "text" | "product",
	value: any
}