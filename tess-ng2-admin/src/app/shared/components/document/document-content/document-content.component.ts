import { ProductItem } from './product-item/product-item.component';
import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'document-content',
	templateUrl: 'document-content.component.html',
	styleUrls: ['./document-content.component.scss'],
})

export class DocumentContentComponent implements OnInit {

	@Input() type: "text" | "product";
	@Input() content: string[] | ProductItem[];

	isText() { 
		return this.type === "text"; 
	}

	priceVariableTot() {
		const tot = (this.content as ProductItem[])
			.reduce((total: any, item: any): any => {
				return total + Number(item.priceVariable);
			}, 0);
		return tot;
	}

	priceFixedTot() {
		const tot = (this.content as ProductItem[])
			.reduce((total: any, item: any): any => {
				return total + Number(item.priceFixed);
			}, 0);
		return tot;
	}

	ngOnInit() { }
	myLog(data) {
		return JSON.stringify(data);
	}
}