import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'document-head',
	templateUrl: 'document-head.component.html',
	styleUrls: ['./document-head.component.scss']
})

export class DocumentHeadComponent implements OnInit {
	@Input() editable: boolean = false;
	ngOnInit() { }
}