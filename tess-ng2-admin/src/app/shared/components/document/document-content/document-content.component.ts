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
		const tot = (this.content as ProductItem[]).reduce((total: any, item: any):any => total + Number(item.priceVariable))
		console.log(tot);
		return tot;
	}

	ngOnInit() { }
}