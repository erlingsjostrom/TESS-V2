import { OfferService } from '../../../shared/resources/offers/offer.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { ProductItem } from "app/shared/components/document/document-content/product-item/product-item.component";

let _html2pdf = html2pdf;

@Component({
	selector: 'view',
	templateUrl: 'view.component.html',
	styleUrls: ['./view.component.scss', '../../../../../node_modules/ng2-dnd/style.css']
})

export class ViewComponent implements OnInit {
	@ViewChild('pdfPage') pdfPage: ElementRef;
	title: string = "View"
	
	private _id: number;
	test: any;
	state = {
		editable: false
	}
	constructor(
		private _route: ActivatedRoute,
		private _router: Router,
		private _offerService: OfferService
	) {}
	
	toggleEditMode() {
		this.state.editable = !this.state.editable;
	}

	ngOnInit() { 
		this._id = +this._route.snapshot.paramMap.get('id');
		//(<any>window).displayPixelRatio = 2; //2 css pixels per screen pixel, can slightly increase pdf quality

		this._offerService.getWithContents(this._id).subscribe(
			response => {
				if (response.status == 200) {
					const offer = response.json();
					const contents = offer.Contents;
					this.content = contents.map( c => {
						if (c.Type === "TextItem") {
							return {
								type: 'text',
								id: c.Id,
								value: c.TextItems
							}
						} else {
							return {
								type: 'product',
								id: c.Id,
								value: c.Articles.map(a => {
									return {
										nr: a.ArticleNumber,
										description: a.Name + " - " + a.Description,
										priceFixed: Math.floor(Math.random() * 1000),
										priceVariable: Math.floor(Math.random() * 500),
									}
								})
							}
						}
					})
					
					console.log(this.content);
				}
			}
		)
	}
	
	edit() {
		this._router.navigate(['offers/edit', this._id]);
	}
	createPdf () {
		var test = html2pdf(this.pdfPage.nativeElement, {
		  margin:       [19, 12, 19, 10],
		  filename:     'offer' + this._id + '.pdf',
		  image:        { type: 'png' },
			html2canvas:  { dpi: 200},
		})
	}
	
	content: Content[] = [];

}

export interface Content {
	type: "text" | "product",
	value: any
}